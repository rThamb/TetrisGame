using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TetrisGame;
using Microsoft.Xna.Framework; 

namespace TetrisGameTest.ShapeTests
{
    [TestClass]
    public class ShapeJTest
    {
        [TestMethod]
        public void MoveDownTest()
        {
            Shape a = createInstance();
            for (int i = 0; i < 14; i++)
            {
                a.DrawShape();

                //move down once
                String expect = calDownLeftOrRight(a, 0, 1);

                //act
                a.MoveDown();
                Console.WriteLine("Expected is \n" + expect + "\n" + "Obtained is \n" + a.ToString());

                Assert.AreEqual(expect, a.ToString());
            }

        }

        [TestMethod]
        public void MoveLeftTest()
        {
            Shape a = createInstance();

            String expect = "";
            //only 4 valid time to move left 
            for (int i = 0; i < 6; i++)
            {

                a.DrawShape();


                if (i <= 3)
                    expect = calDownLeftOrRight(a, -1, 0);
                else
                    expect = calDownLeftOrRight(a, 0, 0);

                //act
                a.MoveLeft();

                //assert 
                Console.WriteLine("Expected is \n" + expect + "\n" + "Obtained is \n" + a.ToString());

                Assert.AreEqual(expect, a.ToString());
            }
        }

        [TestMethod]
        public void MoveRightTest()
        {
            Shape a = createInstance();

            String expect = "";

            //only 3 valid time to move right 
            for (int i = 0; i < 6; i++)
            {

                a.DrawShape();


                if (i <= 2)
                    expect = calDownLeftOrRight(a, 1, 0);
                else
                    expect = calDownLeftOrRight(a, 0, 0);

                //act
                a.MoveRight();

                //assert 
                Console.WriteLine("Expected is \n" + expect + "\n" + "Obtained is \n" + a.ToString());

                Assert.AreEqual(expect, a.ToString());
            }

        }

        [TestMethod]
        public void DropTest()
        {
            Shape a = createInstance();

            Console.WriteLine(a.ToString());

            String expected = "Tetris Shape Coor:\n" + "5,18\n6,18\n6,19\n4,18\n";

            //act
            a.Drop();

            a.DrawShape();
            Assert.AreEqual(expected, a.ToString());
        }

        [TestMethod]
        public void RotateTest()
        {
            Shape a = createInstance();

            // test two offsets 

            //drop by 3 space for the space to have room 

            a.MoveDown();
            a.MoveDown();
            a.MoveDown();

            Point[] rotationSet = {
                                        new Point(0,0),
                                        new Point (-1,-1),
                                        new Point (0,-2),
                                        new Point(1,1)
                                    };

            String expected = calRotate(a, rotationSet);


            //act 

            a.Rotate();

            //assert 

            Console.WriteLine(expected + " " + a.ToString());

            Assert.AreEqual(expected, a.ToString());

            a.MoveDown();
            a.MoveDown();
            a.MoveDown();

            Point[] rotationSet1 = {
                                        new Point(0,0),
                                        new Point (-1,1),
                                        new Point (-2,0),
                                        new Point(1,-1)
                                    };

            String expected2 = calRotate(a, rotationSet1);


            //act 

            a.Rotate();

            //assert 

            Console.WriteLine(expected2 + " " + a.ToString());

            Assert.AreEqual(expected2, a.ToString());


        }

        [TestMethod]
        public void TestReset()
        {
            String expected = "Tetris Shape Coor:\n" + "5,0\n6,0\n6,1\n4,0\n";

            Shape a = createInstance();

            a.MoveDown();
            a.MoveDown();
            a.MoveRight();
            a.Rotate();

            a.DrawShape();
            a.Reset();
            a.DrawShape();

            Assert.AreEqual(expected, a.ToString());

            a.Drop();
            a.DrawShape();
            a.Reset();
            a.DrawShape();

            Assert.AreEqual(expected, a.ToString());

        }



        private Shape createInstance()
        {
            IBoard board = new Board(20, 10);

            return new ShapeJ(board);
        }
        //*********************************************************************************************

        private String calDownLeftOrRight(Shape b4Act, int x_ToMove, int y_ToMove)
        {
            Point[] expectedNums = calChange(getCurrentPosition(b4Act), x_ToMove, y_ToMove);

            String info = "Tetris Shape Coor:\n";

            for (int i = 0; i < expectedNums.Length; i++)
                info += expectedNums[i].X + "," + expectedNums[i].Y + "\n";

            return info;
        }

        private Point[] getCurrentPosition(Shape b4Act)
        {
            Point[] pos = new Point[4];

            for (int i = 0; i < 4; i++)
            {
                pos[i] = b4Act[i].Position;
            }
            return pos;
        }

        private Point[] calChange(Point[] points, int x, int y)
        {
            Point[] expectNums = new Point[4];

            for (int i = 0; i < 4; i++)
            {
                expectNums[i] = new Point((points[i].X + x), (points[i].Y + y));
            }

            return expectNums;
        }

        //******************* Method to test rotate 

        private String calRotate(Shape b4Act, Point[] offset)
        {
            Point[] expectedNums = new Point[4];

            Point[] beforeAct = getCurrentPosition(b4Act);

            for (int i = 0; i < 4; i++)
            {
                int x = beforeAct[i].X + offset[i].X;
                int y = beforeAct[i].Y + offset[i].Y;

                expectedNums[i] = new Point(x, y);
            }

            String info = "Tetris Shape Coor:\n";

            for (int i = 0; i < expectedNums.Length; i++)
                info += expectedNums[i].X + "," + expectedNums[i].Y + "\n";

            return info;

        }

        private Point[] getPositionAfterRotate(Shape afterAct)
        {
            Point[] pos = new Point[4];

            for (int i = 0; i < 4; i++)
            {
                pos[i] = afterAct[i].Position;
            }

            return pos;
        }
        

    }
}
