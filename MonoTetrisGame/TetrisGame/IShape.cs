using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    /// Authors: Dimitri Spyropoulos, Renuchan Thambirajah, Hau Gilles Che
    public interface IShape
    {
        /// <summary>
        /// Access the length of the shape.
        /// </summary>
        int Length
        {
            get;
        }

        /// <summary>
        /// Access the block of a shape.
        /// </summary>
        /// <param name="i">One of the blocks of the shape.</param>
        /// <returns>Indicated block of the shape.</returns>
        Block this[int i]
        {
            get;
        }

        /// <summary>
        /// Event fired when a piece is added to the board.
        /// </summary>
        event JoinPileHandler JoinPile;

        /// <summary>
        /// Moves the shape left by one column.
        /// </summary>
        void MoveLeft();

        /// <summary>
        /// Moves the shape right by one column.
        /// </summary>
        void MoveRight();

        /// <summary>
        /// Moves the shape down by one row.
        /// </summary>
        /// <returns>If the piece has shifted down or not.</returns>
        bool MoveDown();

        /// <summary>
        /// Drops the shape to the lowest point possible.
        /// </summary>
        void Drop();

        /// <summary>
        /// Rotates the shape counterclockwise.
        /// </summary>
        void Rotate();

        /// <summary>
        /// Sets the shape to it's initial position.
        /// </summary>
        void Reset();
    }
}
