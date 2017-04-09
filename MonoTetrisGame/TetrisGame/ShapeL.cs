﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TetrisGame
{
    /// Authors: Dimitri Spyropoulos, Renuchan Thambirajah, Hau Gilles Che
    public class ShapeL : Shape
    {
        public ShapeL(IBoard board)
            : base(board)
        {
            this.currentRotation = 0;
            this.rotationOffset = setRotationOffsets();


            int center = board.GetLength(1) / 2;

            //   (initial State)   
            //          2 0 1         
            //          3

            Block[] position = {
                                   new Block(board, Color.Aquamarine, new Point(center, 0)),
                                   new Block(board, Color.Aquamarine, new Point(center + 1, 0)),
                                   new Block(board, Color.Aquamarine, new Point(center - 1 , 0)),
                                   new Block(board, Color.Aquamarine, new Point(center - 1, 1))
                               };

            this.blocks = position;
        }
        
        private Point[][] setRotationOffsets()
        { 
            //          Initial 

            //
            //          2 0 1         
            //          3  

            //          change to 

            //            1
            //            0          
            //            2 3

            Point[] rotationSet_1 = {
                                        new Point(0,0),
                                        new Point(-1,-1),
                                        new Point(1,1),
                                        new Point(2,0)
                                    };

            //          change to 

            //               3
            //           1 0 2         
            //            

            Point[] rotationSet_2 = {
                                        new Point(0,0),
                                        new Point(-1,1),
                                        new Point(1,-1),
                                        new Point(0,-2)
                                    };

            //          change to 

            //           3 2     
            //             0          
            //             1

            Point[] rotationSet_3 = {
                                        new Point(0,0),
                                        new Point(1,1),
                                        new Point(-1,-1),
                                        new Point(-2,0)
                                    };

            //    initial position 
            //          2 0 1         
            //          3  

            Point[] rotationSet_0 = {
                                        new Point(0,0),
                                        new Point(1,-1),
                                        new Point(-1,1),
                                        new Point(0,2)
                                    };

            Point[][] offset = { rotationSet_0, rotationSet_1, rotationSet_2, rotationSet_3 };

            return offset; 
        }

        /// <summary>
        /// Sets the shape to it's initial position.
        /// </summary>
        public override void Reset() 
        {         
            int center = 5;

            blocks[0].Position = new Point(center, 0);
            blocks[1].Position = new Point(center + 1, 0);
            blocks[2].Position = new Point(center - 1 , 0);
            blocks[3].Position = new Point(center - 1, 1);

            currentRotation = 0;
        }
    }
}
