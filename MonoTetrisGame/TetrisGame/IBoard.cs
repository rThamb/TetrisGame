using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TetrisGame
{
    public delegate void LinesClearedHandler(int lines); 
    public delegate void GameOverHandler();

    /// Authors: Dimitri Spyropoulos, Renuchan Thambirajah, Hau Gilles Che
    public interface IBoard
    {   
        /// <summary>
        /// Access the current shape.
        /// </summary>
        IShape Shape
        {
            get;
        }

        /// <summary>
        /// Access the color of the specified position in the array.
        /// </summary>
        /// <param name="i">Y-coordinate of desired position in the array.</param>
        /// <param name="j">X-coordinate of desired position in the array.</param>
        /// <returns>Color of the specified position in the array.</returns>
        Color this[int i, int j]
        {
            get;
        }
       
        /// <summary>
        /// Event fired when a line is cleared from the board.
        /// </summary>
        event LinesClearedHandler LinesCleared;

        /// <summary>
        /// Event fired when the game is over.
        /// </summary>
        event GameOverHandler GameOver; 

        /// <summary>
        /// Returns the specified dimension of the board.
        /// </summary>
        /// <param name="rank">Dimension of the board.</param>
        /// <returns>The specified dimension.</returns>
        int GetLength(int rank);
    }
}
