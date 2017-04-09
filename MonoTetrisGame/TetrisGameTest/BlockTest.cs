using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TetrisGame;
using Microsoft.Xna.Framework; 


namespace TetrisGameTest
{
    [TestClass]
    public class BlockTest
    {
        [TestMethod]
        public void TryMoveLeftTest()
        {
            // 2,3

            /*
             *  Will shift the block 4 space to the left  
             */

            Console.WriteLine("\nTesting TryMoveLeft()\n");

            //arrange
            Block block = createInstance(2,3);

         
            //act
            //act
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Checking Position " + block);

                bool result = block.TryMoveLeft();
                Console.WriteLine("TryMoveLeft() = " + result);

                int x = block.Position.X;
                int y = block.Position.Y;

                block.Position = new Point((x - 1), y);
                Console.WriteLine("Moving to Location " + block);

               Console.WriteLine("\n");


                //only 2 valid moves left 
                if (i >= 2)
                    Assert.AreEqual(false, result);
                else
                    Assert.AreEqual(true, result);
 
            }
            

        }

        [TestMethod]
        public void TryMoveRightTest()
        {

            /*
             *  Will shift the block 4 spaces to the right  
             */

            Console.WriteLine("\nTesting TryMoveRight()\n");

            //arrange
            Block block = createInstance(7, 5);


            //act
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Checking Position " + block);

                bool result = block.TryMoveRight();
                Console.WriteLine("TryMoveRight() = " + result);

                int x = block.Position.X;
                int y = block.Position.Y;

                block.Position = new Point((x + 1), y);
                Console.WriteLine("Moving to Location, " + block);

                Console.WriteLine("\n");

                if (i >= 2)
                    Assert.AreEqual(false, result);
                else
                    Assert.AreEqual(true, result);
 
            }

        }
        [TestMethod]
        public void TryMoveDownTest()
        {
            Console.WriteLine("\nTesting TryMoveDown()\n");

            Block block = createInstance(9, 17);

            /*
             *  Only 2 more valid move down are available 
             * 
             */

            //act
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Checking Position " + block);

                bool result = block.TryMoveDown();
                Console.WriteLine("TryMoveDown() = " + result);

                int x = block.Position.X;
                int y = block.Position.Y;

                block.Position = new Point(x, (y+1));
                Console.WriteLine("Moving to Location, " + block);

                Console.WriteLine("\n");

                if (i >= 2)
                    Assert.AreEqual(false, result);
                else
                    Assert.AreEqual(true, result);

            }
        }
        [TestMethod]
        public void TryRotateTest()
        {
            Console.WriteLine("\nTesting TryRotate()\n");

            Block block = createInstance(5, 5);
            Point offset = new Point(4,4);

            Assert.AreEqual(true, block.TryRotate(offset));

            block = createInstance(9, 9);
            offset = new Point(1, 2);
            
            Assert.AreEqual(false, block.TryRotate(offset));

            block = createInstance(9, 0);
            offset = new Point(0, 1);

            Assert.AreEqual(true, block.TryRotate(offset));

            block = createInstance(9, 0);
            offset = new Point(1, 0);

            Assert.AreEqual(false, block.TryRotate(offset));


        }
        [TestMethod]
        public void MoveLeftTest()
        {
            Console.WriteLine("\nTesting MoveLeft()\n");

            /*
             *  Will check to see if only if the block is shifting 1 space to the left,
             *  
             * checking if its valid is the tryMoveLeft responsiblity 
             * 
             */

            Random num = new Random();

            for (int i = 0; i < 10; i++)
            {
                //arrange
                Block block = createInstance(num.Next(1, 10), num.Next(0, 20));
                Console.WriteLine(block);
                Point expected = block.Position;

                // act 
                block.MoveLeft();

                expected = expectedBlock(expected, new Point(-1, 0));

                Console.WriteLine("Expected = " + expected.X + ","  + expected.Y + "\nObtained = " + block.Position.X + "," + block.Position.Y + "\n");

                Assert.AreEqual(expected, block.Position);
            }

        }

        private Point expectedBlock(Point expected, Point offset)
        {
            int x = expected.X + offset.X;
            int y = expected.Y + offset.Y;


            return new Point(x, y);

        }
        [TestMethod]
        public void MoveRightTest()
        {
            Console.WriteLine("\nTesting MoveRight()\n");

            Random num = new Random();

            for (int i = 0; i < 10; i++)
            {
                //arrange
                Block block = createInstance(num.Next(0, 8), num.Next(0, 20));
                Console.WriteLine(block);
                Point expected = block.Position;

                // act 
                block.MoveRight();

                expected = expectedBlock(expected, new Point(1, 0));

                Console.WriteLine("Expected = " + expected.X + "," + expected.Y + "\nObtained = " + block.Position.X + "," + block.Position.Y + "\n");

                Assert.AreEqual(expected, block.Position);
            }
        }
        [TestMethod]
        public void MoveDownTest()
        {
            Console.WriteLine("\nTesting MoveDown()\n");

            Random num = new Random();

            for (int i = 0; i < 10; i++)
            {
                //arrange
                Block block = createInstance(num.Next(0, 10), num.Next(0, 19));
                Console.WriteLine(block);
                Point expected = block.Position;

                // act 
                block.MoveDown();

                expected = expectedBlock(expected, new Point(0, 1));

                Console.WriteLine("Expected = " + expected.X + "," + expected.Y + "\nObtained = " + block.Position.X + "," + block.Position.Y + "\n");

                Assert.AreEqual(expected, block.Position);
            }
        }
        [TestMethod]
        public void RotateTest()
        {
            Console.WriteLine("\nTesting Rotate()\n");

            Random num = new Random();

            for (int i = 0; i < 10; i++)
            {
                //arrange
                Block block = createInstance(num.Next(0, 10), num.Next(0, 19));
                Console.WriteLine(block);
                Point expected = block.Position;
                Point offset = new Point(num.Next(-5, 5), num.Next(-5, 5));
                Console.WriteLine("Offset = " + offset); 

                // act 
                block.Rotate(offset);

                expected = expectedBlock(expected, offset);

                Console.WriteLine("Expected = " + expected.X + "," + expected.Y + "\nObtained = " + block.Position.X + "," + block.Position.Y + "\n");

                Assert.AreEqual(expected, block.Position);
            }
            
        }



        [TestMethod]
        public void ToStringTest()
        {
        }

        private Block createInstance(int x, int y)
        {
            IBoard board = new Board(20, 10);

            Point a = new Point(x,y);

            return new Block(board, Color.Red, a); 
        }
        

    }
}
