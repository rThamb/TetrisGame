using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Xna.Framework;

namespace TetrisGame
{
    public delegate void JoinPileHandler();

    /// Authors: Dimitri Spyropoulos, Renuchan Thambirajah, Hau Gilles Che
    public abstract class Shape : IShape
    {
        public event JoinPileHandler JoinPile;
        private IBoard board;
        protected Block[] blocks;
        protected Point[][] rotationOffset;
        protected int currentRotation = 0;
         
        public Shape(IBoard board)
        {
            if (board != null)
                this.board = board;
            else
                throw new ArgumentNullException("Sent a null Board");
         
        }

        /// <summary>
        /// Fires JoinPile event.
        /// </summary>
        protected virtual void OnJoinPile()
        {
            if (JoinPile != null)
                JoinPile();
        }

        /// <summary>
        /// Access the length of the shape.
        /// </summary>
        public int Length
        {
            get { return blocks.Length; }
        }

        /// <summary>
        /// Access the block of a shape.
        /// </summary>
        /// <param name="coordinates">One of the blocks of the shape.</param>
        /// <returns>Indicated block of the shape.</returns>
        public Block this[int coordinates]
        {
            get
            {
                if (coordinates < 0 || coordinates >= blocks.Length)
                    throw new IndexOutOfRangeException("Index Not in range");
                else
                    return blocks[coordinates];
            }
        }

        /// <summary>
        /// Moves the shape left by one column.
        /// </summary>
        public void MoveLeft()
        {
            bool canAllMove = true;  

            //check all the blocks 
            for (int i = 0; i < blocks.Length; i++)
            {
                if (!(blocks[i].TryMoveLeft()))
                { 
                    canAllMove = false; 
                }
            }

            if (canAllMove)
            {
                for (int i = 0; i < blocks.Length; i++)
                    blocks[i].MoveLeft();
            }
        }

        /// <summary>
        /// Moves the shape right by one column.
        /// </summary>
        public void MoveRight()
        {
            bool canAllMove = true;

            //check all the blocks 
            for (int i = 0; i < blocks.Length; i++)
            {
                if (!blocks[i].TryMoveRight())
                    canAllMove = false;
            }

            if (canAllMove)
            {
                for (int i = 0; i < blocks.Length; i++)
                    blocks[i].MoveRight();
            }
        }

        /// <summary>
        /// Moves the shape down by one row.
        /// </summary>
        /// <returns>If the piece has shifted down or not.</returns>
        public bool MoveDown()
        {           
            //check all the blocks 
            for (int i = 0; i < blocks.Length; i++)
            {
                if (!(blocks[i].TryMoveDown()))
                {
                    //hit the bottom fire JoinPile
                    OnJoinPile();
                    return false;
                }
            }

            //move the block down 1 square 
            for (int i = 0; i < blocks.Length; i++)
                blocks[i].MoveDown();

            return true;
        }

        /// <summary>
        /// Drops the shape to the lowest point possible.
        /// </summary>
        public void Drop()
        {
            while (canAllMoveDown())
            {
                for (int i = 0; i < blocks.Length; i++)
                    blocks[i].MoveDown();
            }
            //at the lowest point add to pile
            OnJoinPile();
        }
        
        /// <summary>
        /// Returns whether all blocks can shift down one position or not. 
        /// </summary>
        /// <returns>If all blocks can shift down one position or not.</returns>
        private bool canAllMoveDown()
        {
            for (int i = 0; i < blocks.Length; i++)
            {
                if (!(blocks[i].TryMoveDown()))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Rotates the shape counterclockwise.
        /// </summary>
        public virtual void Rotate()
        {
            int tempInt = currentRotation + 1; 

            int rotateSets = rotationOffset.GetLength(0);

            int rotationSet = tempInt % rotateSets;

            bool canAllMove = true;

            for (int i = 0; i < blocks.Length; i++)
            {
                if (!(blocks[i].TryRotate(rotationOffset[rotationSet][i])))
                    canAllMove = false;
            }

            if (canAllMove)
            {
                this.currentRotation++;
                rotationSet = this.currentRotation % rotateSets;

                for (int i = 1; i < blocks.Length; i++)
                {
                    Point currentPosition = blocks[i].Position;

                    //add the offset to the point
                    Point newPosition = new Point(
                                                    currentPosition.X + (rotationOffset[rotationSet][i].X),
                                                    currentPosition.Y + (rotationOffset[rotationSet][i].Y)
                                                 );

                    //set the change 
                    blocks[i].Position = newPosition;
                }
                
            }
        }

        /// <summary>
        /// Returns a string representation of a shape.
        /// </summary>
        /// <returns>String representation of a shape.</returns>
        public override String ToString()
        {
            String info = "Tetris Shape Coor:\n";

            for (int i = 0; i < blocks.Length; i++)
                info += blocks[i].Position.X + "," + blocks[i].Position.Y + "\n";

            return info;
        }

        /// <summary>
        /// Sets the shape to it's initial position.
        /// </summary>
        public abstract void Reset();

    }
}
