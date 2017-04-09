using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TetrisGame;
using Microsoft.Xna.Framework; 

namespace TetrisGameTest
{
    [TestClass]
    public class ShapeProxyTest
    {
        private FakeBoard boardInstance;  

        [TestMethod]
        public void TestLengthProperty()
        {
            ShapeProxy a = createInstance();

            int num = a.Length;

            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(4, num);

            }
        }

        [TestMethod]
        public void TestIndexerProperty()
        {
            ShapeProxy a = createInstance();

            for (int i = 0; i < 4; i++)
            {
                Assert.AreNotEqual(a[i], null); 

            }
        }

        [TestMethod]
        public void MoveDownProxyTest()
        {
            ShapeProxy b = createInstance();

            Shape a = boardInstance.Shape as Shape;

            for (int i = 0; i < 5; i++)
            {
                //move down once
                String expect = calDownLeftOrRight(a, 0, 1);

                //act
                b.MoveDown();
                a.DrawShape();
                Console.WriteLine("Expected is \n" + expect + "\n" + "Obtained is \n" + a.ToString());



                Assert.AreEqual(expect, b.ToString());
            }


        }

        [TestMethod]
        public void MoveLeftProxyTest()
        {
            ShapeProxy b = createInstance();

            Shape a = boardInstance.Shape as Shape;

            //two places down 
            for (int i = 0; i < 2; i++)
            {
                //move down once
                String expect = calDownLeftOrRight(a, -1, 0);

                //act
                b.MoveLeft();

                a.DrawShape();
                Console.WriteLine("Expected is \n" + expect + "\n" + "Obtained is \n" + a.ToString());

                Assert.AreEqual(expect, b.ToString());
            }


        }

        [TestMethod]
        public void MoveRightProxyTest()
        {
            ShapeProxy b = createInstance();

            Shape a = boardInstance.Shape as Shape;

            //two places down 
            for (int i = 0; i < 2; i++)
            {
                //move down once
                String expect = calDownLeftOrRight(a, 1, 0);

                //act
                b.MoveRight();

                a.DrawShape();
                Console.WriteLine("Expected is \n" + expect + "\n" + "Obtained is \n" + a.ToString());

                Assert.AreEqual(expect, b.ToString());
            }
        }

        [TestMethod]
        public void DropProxyTest()
        {
            ShapeProxy b = createInstance();

            bool bottomLineFilled = false;

            //to avoid clearlines if ShapeI spawns 
            boardInstance.board[19, 0] = Color.Transparent;

            //two places down 
            Console.WriteLine("Board before drop\n"); 
            UtilityClass.DrawFakeBoardContents(boardInstance);
                //move down once
            //act
            b.Drop();

            String afterDrop = b.ToString();
            Console.WriteLine("board After drop\n");
            

            UtilityClass.DrawFakeBoardContents(boardInstance);

            if (boardInstance[19, 3] != Color.Transparent ||
                boardInstance[19, 4] != Color.Transparent ||
                boardInstance[19, 5] != Color.Transparent ||
                boardInstance[19, 6] != Color.Transparent)
                bottomLineFilled = true;
            else
                bottomLineFilled = false;

            Assert.AreEqual(true, bottomLineFilled);

        }

        [TestMethod]
        public void MoveResetProxyTest()
        {
            ShapeProxy b = createInstance();

            b.MoveDown();
            String beforeReset = b.ToString();
            //two places down 

            //move down once
            //act
            b.Reset();

            String afterReset = b.ToString();
            
            Console.WriteLine("Coors should be different" + "Before Reset" + beforeReset + "\nAfter Reset" + afterReset);

            Assert.AreNotEqual(beforeReset, afterReset);

        }

        [TestMethod]
        public void TestDeployShape()
        {
            ShapeProxy a = createInstance();

            //test Shape returns not nulls and valid shapes 

            var shape = a.DeployNewShape();
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(true, testValidShape(shape));
                shape = a.DeployNewShape();
            }
        }

        private bool testValidShape(IShape shape)
        {
            Shape[] shapes = {
                                 new ShapeI(boardInstance),
                                 new ShapeJ(boardInstance),
                                 new ShapeL(boardInstance),
                                 new ShapeO(boardInstance),
                                 new ShapeS(boardInstance),
                                 new ShapeT(boardInstance),
                                 new ShapeZ(boardInstance)
                             };

            foreach (var item in shapes)
            {
                if (item.ToString().Equals(shape.ToString()))
                    return true; 
            }

            return false;
        }

        [TestMethod]
        public void MoveRotateProxyTest()
        {
            ShapeProxy b = createInstance();

            b.MoveDown();
            b.MoveDown();
            b.MoveDown();

            String beforeRotate = b.ToString();

            b.Rotate();
            Console.WriteLine(b.ToString() + "before Rotate\n" + beforeRotate);

           //back to inital state

            //if shape is an O no rotation is available
            if(boardInstance.Shape is ShapeO)
                Assert.AreEqual(beforeRotate, b.ToString()); 
            else
                Assert.AreNotEqual(beforeRotate, b.ToString());     
        }


        // **************************** Test Move *******************************
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


        //************************************************************************************
        private ShapeProxy createInstance()
        {
            IBoard board = new FakeBoard(20, 10);

            boardInstance = (FakeBoard)board;

            FakeBoard fake = board as FakeBoard;

            fake.board[19, 0] = Color.DarkBlue;
            fake.board[19, 1] = Color.DarkBlue;
            fake.board[19, 2] = Color.DarkBlue;
            fake.board[19, 7] = Color.DarkBlue;
            fake.board[19, 8] = Color.DarkBlue;
            fake.board[19, 9] = Color.DarkBlue;


            ShapeProxy factory = (ShapeProxy)((FakeBoard)board).shapeFactory;

            return factory;
        }
    }
}
