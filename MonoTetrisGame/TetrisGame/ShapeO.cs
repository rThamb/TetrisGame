using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TetrisGame
{
    ///Authors: Dimitri Spyropoulos, Renuchan Thambirajah, Hau Gilles Che
    public class ShapeO : Shape
    {
        public ShapeO(IBoard board)
            : base (board)
        {
            this.currentRotation = 0;
            this.rotationOffset = null;

            int center = board.GetLength(1) / 2;

            //initial position 

            //  0 1
            //  2 3
        
            Block[] position = {
                                   new Block(board, Color.Crimson, new Point(center - 1, 0)),
                                   new Block(board, Color.Crimson, new Point(center, 0)),
                                   new Block(board, Color.Crimson, new Point(center - 1 , 1)),
                                   new Block(board, Color.Crimson, new Point(center, 1))
                               };

            this.blocks = position;
        }

        /// <summary>
        /// Rotates the shape counterclockwise.
        /// </summary>
        public override void Rotate()
        {
            //don't do anything 
            return;
        }

        /// <summary>
        /// Sets the shape to it's initial position.
        /// </summary>
        public override void Reset()
        {
            int center = 5;

            blocks[0].Position = new Point(center - 1, 0);
            blocks[1].Position = new Point(center, 0);
            blocks[2].Position = new Point(center - 1, 1);
            blocks[3].Position = new Point(center, 1);

            currentRotation = 0;
        }
    }
}
