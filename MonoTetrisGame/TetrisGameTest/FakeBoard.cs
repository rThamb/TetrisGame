using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGame; 
using Microsoft.Xna.Framework; 

namespace TetrisGameTest
{
    public class FakeBoard : IBoard
    {
        public event LinesClearedHandler LinesCleared;
        public event GameOverHandler GameOver;
        public Color[,] board;
        public IShape shape;
        public IShapeFactory shapeFactory;

        public IShape Shape
        {
            get { return this.shape; }
        }


        public Color this[int y, int x]
        {
            get { return board[y, x]; }
        }

        public FakeBoard(int length, int width)
        {
            board = new Color[length, width];
            shapeFactory = new ShapeProxy(this);
            shape = shapeFactory.DeployNewShape();
            (shapeFactory as ShapeProxy).JoinPile += addToPile;


        }

        public int GetLength(int rank)
        {
            return board.GetLength(rank);
        }

        protected void OnLinesCleared(int lines)
        {
            if (LinesCleared != null)
                LinesCleared(lines);
        }

        protected void OnGameOver()
        {
            if (GameOver != null)
                GameOver();
        }


        public void addToPile()
        {          
            for (int i = 0; i < shape.Length; i++)
                if (board[shape[i].Position.Y, shape[i].Position.X] != Color.Transparent)
                {
                    OnGameOver();
                }

            Color shapeColor = shape[0].Colour;

            for (int i = 0; i < shape.Length; i++)
            {
                board[shape[i].Position.Y, shape[i].Position.X] = shapeColor;
            }

            int[] fullLinesCoord = checkFullLines();


            Console.WriteLine(fullLinesCoord.Length);
            if (fullLinesCoord.Length > 0)
            {
                clearLines(fullLinesCoord);

                OnLinesCleared(fullLinesCoord.Length);
            }
            shape = shapeFactory.DeployNewShape();

        }

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

        private void clearLines(int[] yCoords)
        {
            int lineCtr = board.GetLength(0)-1;
            int boardLength = board.GetLength(1);
            Color[,] nBoard = new Color[board.GetLength(0),board.GetLength(1)];

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

        public void invokeIncrementLine(int lines)
        {
            OnLinesCleared(lines);
        }


        public override string ToString()
        {
            return "Length = " + (board.GetLength(0)) + "Width =" + board.GetLength(1);
        }

    }
}
