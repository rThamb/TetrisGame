using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TetrisGame
{
    /// Authors: Dimitri Spyropoulos, Renuchan Thambirajah, Hau Gilles Che
    public class Block
    {
        private IBoard board;
        private Color colour;
        private Point position;

        /// <summary>
        /// Access the color of block.
        /// </summary>
        public Color Colour
        {
            get { return colour; }
        }

        /// <summary>
        /// Access and modify the position of the block.
        /// </summary>
        public Point Position 
        {
            get { return position; }
            set 
            {
                if (value == null)
                    throw new ArgumentException("(Block Class) Tried to set a null Point");
                else
                    position = value;
            }
        }

        public Block(IBoard board, Color colour, Point position)
        {
            if (board == null || colour == null || position == null)
                throw new ArgumentException("(Block Class) Sent a null value to Block CTNR");

            else
            {
                this.board = board;
                this.colour = colour;
                this.position = position;
            }
        }

        /// <summary>
        /// Checks if the block is able to move left.
        /// </summary>
        /// <returns>If the block is able to move left or not.</returns>
        public bool TryMoveLeft()
        { 
            int current_X = position.X;
            int current_Y = position.Y;

            try
            {
                if (board[(current_Y) , (current_X - 1)] == Color.Transparent) 
                    return true;

                else
                    return false;
            }
            catch (IndexOutOfRangeException e)
            {              
                return false;
            }
        }

        /// <summary>
        /// Checks if the block is able to move right.
        /// </summary>
        /// <returns>If the block is able to move right or not.</returns>
        public bool TryMoveRight()
        {
            int current_X = position.X;
            int current_Y = position.Y;

            try
            {
                if (board[current_Y, (current_X + 1)] == Color.Transparent) 
                    return true;
                else
                    return false; 
            }
            catch (IndexOutOfRangeException e)
            {
                return false;
            }      
        }

        /// <summary>
        /// Checks if the block is able to move down.
        /// </summary>
        /// <returns>If the block is able to move down or not.</returns>
        public bool TryMoveDown()
        { 
            int current_X = position.X;
            int current_Y = position.Y;

            try
            {
                if (board[(current_Y + 1), current_X] == Color.Transparent)
                    return true;
                else
                    return false;
            }
            catch (IndexOutOfRangeException e)
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the block can shift to the offset.
        /// </summary>
        /// <param name="offset">Point to move the block to.</param>
        /// <returns>If the block can shift to the offset or not.</returns>
        public bool TryRotate(Point offset)
        {
            int new_X_Location = position.X + offset.X;
            int new_Y_Location = position.Y + offset.Y;

            try
            {
                if (board[new_Y_Location, new_X_Location] == Color.Transparent)
                    return true;
                else
                    return false;
            }
            catch (IndexOutOfRangeException e)
            {
                return false;
            }
        }

        /// <summary>
        /// Moves the shape left by one column.
        /// </summary>
        public void MoveLeft()
        {
            position.X = position.X - 1;
        }

        /// <summary>
        /// Moves the shape right by one column.
        /// </summary>
        public void MoveRight()
        {
            position.X = position.X + 1;
        }

        /// <summary>
        /// Moves the shape down by one row.
        /// </summary>
        public void MoveDown()
        {
            position.Y = position.Y + 1;
        }

        /// <summary>
        /// Shifts the block to the offset.
        /// </summary>
        /// <param name="offset">Point to move the block to.</param>
        public void Rotate(Point offset)
        {
            position.X += offset.X;
            position.Y += offset.Y;    
        }

        /// <summary>
        /// Returns a string representation of the block.
        /// </summary>
        /// <returns>A string representation of the block.</returns>
        public override String ToString()
        {
            return "Point(x,y) : (" + position.X + "," + position.Y + ") ";
        }

    }
}
