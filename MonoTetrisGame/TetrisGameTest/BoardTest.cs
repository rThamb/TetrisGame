using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using TetrisGame; 

namespace TetrisGameTest
{
    [TestClass]
    public class BoardTest
    {
        /*
         * We shall use a fake board which has the same code as board but just has 
         * public class memebers, we'll test in this method since we have no actual way 
         * to assign a Proxy or get a handle to the proxy  
         */


        [TestMethod]
        public void TestBoardShapeProperty()
        {
            IBoard a = createInstance();

            IShape b = ((FakeBoard)a).shape;

            Assert.AreEqual(b, a.Shape);
        }


        [TestMethod]
        public void TestBoardIndexer()
        {
            IBoard a = createInstance();

            Assert.AreEqual(a[19, 2], Color.DarkRed);

            Assert.AreEqual(a[11, 3], Color.Transparent);
        }

        [TestMethod]
        public void TestBoardGetLength()
        {
            IBoard a = createInstance();

            int length = a.GetLength(0);
            int width = a.GetLength(1);

            Assert.AreEqual(length, 20);

            Assert.AreEqual(width, 10);
        }


        [TestMethod]
        public void TestBoardAddToPile()
        {
            FakeBoard board = createInstance() as FakeBoard;

            ShapeProxy a = getProxy(board);

            //make it so IShape wont clear line when dropped 
            board.board[19, 0] = Color.Transparent;

            int index = 19; 
            bool filled = false;

            IShape current = board.shape;

            Console.WriteLine("Board before the drop");
            UtilityClass.DrawFakeBoardContents(board);
            Console.WriteLine();


            for (int i = 0; i < 12; i++)
            {
                a.Drop();

                Console.WriteLine("Board AFTER the drop");
                UtilityClass.DrawFakeBoardContents(board);

                //check the middle or 1 to the right or left 

                if (board[index, 5] != Color.Transparent ||
                    board[index, 5 + 1] != Color.Transparent ||
                    board[index, 5 - 1] != Color.Transparent)
                    filled = true;
                else
                    filled = false;

                
                index = index - 1;
                Console.WriteLine("Checking index = " + index);

                Assert.AreEqual(true, filled); 
            }
        }

        [TestMethod]
        public void TestClearLines()
        {
            FakeBoard c = createInstance() as FakeBoard;

            IShape current = null;

            while (!(current is ShapeI))
            {
                c = createInstance() as FakeBoard;

                current = c.shape; 
            }

            ShapeProxy z = getProxy(c);

            Console.WriteLine("Board before drop of ShapeI");
            UtilityClass.DrawFakeBoardContents(c); 
            
            z.Drop();

            Console.WriteLine("Button line should be clear and everthing should shft");
            UtilityClass.DrawFakeBoardContents(c);

            //check if line got cleared
            Assert.AreEqual(c[19, 0], Color.Transparent);

            //check if the top piece shifted 1 down 

            Assert.AreEqual(c[1, 0], Color.DarkRed);

        }

        [TestMethod]
        public void TestGameOver()
        {
            FakeBoard board = createInstance() as FakeBoard;

            board.GameOver += FakeGameOverHandler;

            ShapeProxy z = getProxy(board);

            //shapes will be dropped so after 6-7 pieces the board should be full 
            for (int i = 0; i < 15; i++)
            {
                z.Drop();
                UtilityClass.DrawFakeBoardContents(board); 
            }
        }

        private void FakeGameOverHandler()
        {
            Console.WriteLine("GAME OVER");
            Assert.AreEqual(true, true); 
        }

        public IBoard createInstance()
        {
            FakeBoard b = new FakeBoard(20,10);

            b.board[19, 0] = Color.DarkRed;
            b.board[19, 1] = Color.DarkRed;
            b.board[19, 2] = Color.DarkRed; 

            b.board[19, 7] = Color.DarkRed;
            b.board[19, 8] = Color.DarkRed;
            b.board[19, 9] = Color.DarkRed;


            b.board[0, 0] = Color.DarkRed;
            b.board[0, 1] = Color.DarkRed;

            b.board[0, 8] = Color.DarkRed;
            b.board[0, 9] = Color.DarkRed;

            return b; 

        }

        private ShapeProxy getProxy(FakeBoard a)
        {
            return a.shapeFactory as ShapeProxy; 
        }

    }
}
