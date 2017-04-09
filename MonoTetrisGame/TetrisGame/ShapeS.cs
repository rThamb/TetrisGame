using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TetrisGame
{
    /// Authors: Dimitri Spyropoulos, Renuchan Thambirajah, Hau Gilles Che
    public class ShapeS : Shape
    {
        public ShapeS(IBoard board)
            : base(board)
        {
            this.currentRotation = 0;
            this.rotationOffset = setRotationOffsets();

            int center = board.GetLength(1) / 2;

            //Initial State   
            //                 0 1
            //               3 2 

            Block[] position = {
                                   new Block(board, Color.Fuchsia, new Point(center - 1, 0)),
                                   new Block(board, Color.Fuchsia, new Point(center, 0)),
                                   new Block(board, Color.Fuchsia, new Point(center - 1 , 1)),
                                   new Block(board, Color.Fuchsia, new Point(center - 2, 1))
                               };

            this.blocks = position;

        }

        private Point[][] setRotationOffsets()
        { 
            // there are only two rotations for the S shape 

            //block representation with index (the shapes initial state)
            //      
            //                 0 1
            //               3 2              

            // zero will remain constant, the rest will rotate around it

            //points will get added to the current point when rotating the blocks 


            //odd rotations 

            //                 1
            //                 0 2
            //                   3
            
            Point[] rotation_1 = {
                                     new Point(0,0), 
                                     new Point (-1,-1),
                                     new Point(1,-1),
                                     new Point (2,0)
                                 }; 

            //even rotations 

            //      
            //                 0 1
            //               3 2   

            Point[] rotation_0 = {
                                     new Point(0,0),
                                     new Point(1,1),
                                     new Point(-1,1),
                                     new Point(-2,0)
                                 };

            Point[][] offsets = {
                                    rotation_0,
                                    rotation_1
                                };
            return offsets; 
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
            blocks[3].Position = new Point(center - 2, 1);

            currentRotation = 0;
        }
    }
}
