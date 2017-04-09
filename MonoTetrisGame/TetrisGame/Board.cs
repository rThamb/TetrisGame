using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TetrisGame
{
    /// Authors: Dimitri Spyropoulos, Renuchan Thambirajah, Hau Gilles Che
    public class Board : IBoard
    {
        public event LinesClearedHandler LinesCleared;
        public event GameOverHandler GameOver;
        private Color[,] board;
        private IShape shape;
        private IShapeFactory shapeFactory;

        /// <summary>
        /// Access the current shape.
        /// </summary>
        public IShape Shape
        {
            get { return this.shape; }
        }

        /// <summary>
        /// Access the color of the specified position in the array.
        /// </summary>
        /// <param name="y">Y-coordinate of desired position in the array.</param>
        /// <param name="x">X-coordinate of desired position in the array.</param>
        /// <returns>Color of the specified position in the array.</returns>
        public Color this[int y, int x]
        {
            get { return board[y, x]; }
        }

        public Board(int length, int width)
        {
            if (length < 20 || width < 10)
                throw new ArgumentException("Board Size is too small");

            board = new Color[length, width];
            shapeFactory = new ShapeProxy(this);
            shape = shapeFactory.DeployNewShape();
            (shapeFactory as ShapeProxy).JoinPile += addToPile;
        }

        /// <summary>
        /// Returns the specified dimension of the board.
        /// </summary>
        /// <param name="rank">Dimension of the board.</param>
        /// <returns>The specified dimension.</returns>
        public int GetLength(int rank)
        {
            return board.GetLength(rank);
        }

        /// <summary>
        /// Fires LinesCleared event.
        /// </summary>
        /// <param name="lines">Amount of lines cleared.</param>
        protected void OnLinesCleared(int lines)
        {
            if (LinesCleared != null)
                LinesCleared(lines);
        }

        /// <summary>
        /// Fires GameOver event.
        /// </summary>
        protected void OnGameOver()
        {
            if (GameOver != null)
                GameOver();
        }

        /// <summary>
        /// Adds the current piece to the board.
        /// </summary>
        private void addToPile()
        {
            for (int i = 0; i < shape.Length; i++)
                if (board[shape[i].Position.Y, shape[i].Position.X] != Color.Transparent)
                {
                    OnGameOver();
                    return;
                }
                    

            Color shapeColor = shape[0].Colour;

            for (int i = 0; i < shape.Length; i++)
                board[shape[i].Position.Y, shape[i].Position.X] = shapeColor;

            int[] fullLinesCoord = checkFullLines();

            if (fullLinesCoord.Length > 0)
            {
                clearLines(fullLinesCoord);

                OnLinesCleared(fullLinesCoord.Length);
            }

            shape = shapeFactory.DeployNewShape();
        }

        /// <summary>
        /// Checks if there are rows that are full.
        /// </summary>
        /// <returns>Array containing the Y-coordinates of the rows that need to be cleared.</returns>
        private int[] checkFullLines()
        {
            List<int> yCoords = new List<int>(shape.Length);
            List<int> fullLineCoords = new List<int>();
            int boardLength = board.GetLength(1);
            bool isLineFull = true;
            for (int i = 0; i < shape.Length; i++)
            {
                if (!yCoords.Contains(shape[i].Position.Y))
                    yCoords.Add(shape[i].Position.Y);
            }

            for (int i = 0; i < yCoords.Count; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    if (board[yCoords[i], j] == Color.Transparent)
                        isLineFull = false;
                }
                if (isLineFull)
                    fullLineCoords.Add(yCoords[i]);
                isLineFull = true;
            }
            return fullLineCoords.ToArray();
        }

        /// <summary>
        /// Clears the lines of rows that are full.
        /// </summary>
        /// <param name="yCoords">Array containing the Y-coordinates of the rows that need to be cleared.</param>
        private void clearLines(int[] yCoords)
        {
            int lineCtr = board.GetLength(0) - 1;
            int boardLength = board.GetLength(1);
            Color[,] nBoard = new Color[board.GetLength(0), board.GetLength(1)];

            for (int i = lineCtr; i >= 0; i--)
            {
                if (!yCoords.Contains(i))
                {
                    for (int j = 0; j < boardLength; j++)
                    {
                        nBoard[lineCtr, j] = board[i, j];
                    }
                    lineCtr--;
                }
            }
            board = nBoard;
        }

        /// <summary>
        /// Returns a string representation of the board.
        /// </summary>
        /// <returns>Width and length of board.</returns>
        public override string ToString()
        {
            return "Length = " + (board.GetLength(0)) + "Width =" + board.GetLength(1);
        }

    }
}
