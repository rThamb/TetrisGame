using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    /// Authors: Dimitri Spyropoulos, Renuchan Thambirajah, Hau Gilles Che
    public class ShapeProxy : IShapeFactory,IShape
    {
        public event JoinPileHandler JoinPile;
        private Random random;
        private IShape current;
        private IBoard board;
        private IShape[] shapes;

        /// <summary>
        /// Access the length of the shape.
        /// </summary>
        public int Length
        {
            get { return current.Length; }
        }

        /// <summary>
        /// Access the block of a shape.
        /// </summary>
        /// <param name="i">One of the blocks of the shape.</param>
        /// <returns>Indicated block of the shape.</returns>
        public Block this[int i]
        {
            get { return current[i]; }
        }

        public ShapeProxy(IBoard board)
        {
            this.board = board;
            random = new Random();
            IShape[] shapes = {
                                new ShapeI(board), new ShapeJ(board),
                                new ShapeL(board), new ShapeO(board),
                                new ShapeS(board), new ShapeT(board),
                                new ShapeZ(board)
                            };

            foreach (var item in shapes)
            {
                item.JoinPile += OnJoinPile;
            }

            this.shapes = shapes; 
        }

        /// <summary>
        /// Fires JoinPile event.
        /// </summary>
        protected void OnJoinPile()
        {
            if (JoinPile != null)
                JoinPile();
        }

        /// <summary>
        /// Deploys a new shape onto the board.
        /// </summary>
        /// <returns>Randomly generated shape.</returns>
        public IShape DeployNewShape()
        {         
            int shapeNum = random.Next(0, 7);
            current = shapes[shapeNum];
            current.Reset();
            return current;         
        }

        /// <summary>
        /// Moves the shape left by one column.
        /// </summary>
        public void MoveLeft()
        {
            current.MoveLeft();
        }

        /// <summary>
        /// Moves the shape right by one column.
        /// </summary>
        public void MoveRight()
        {
            current.MoveRight();
        }

        /// <summary>
        /// Moves the shape down by one row.
        /// </summary>
        /// <returns>If the piece has shifted down or not.</returns>
        public bool MoveDown()
        {
           return current.MoveDown();
        }

        /// <summary>
        /// Drops the shape to the lowest point possible.
        /// </summary>
        public void Drop()
        {
            current.Drop();
        }

        /// <summary>
        /// Rotates the shape counterclockwise.
        /// </summary>
        public void Rotate()
        {
            current.Rotate();
        }

        /// <summary>
        /// Sets the shape to it's initial position.
        /// </summary>
        public void Reset()
        {
            current.Reset();
        }

        /// <summary>
        /// Returns a string representation of the current shape.
        /// </summary>
        /// <returns>A string representation of the current shape.</returns>
        public override string ToString()
        {
            return current.ToString();
        } 
    }
}
