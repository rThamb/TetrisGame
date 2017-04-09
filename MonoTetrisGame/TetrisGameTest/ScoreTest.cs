using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TetrisGame; 

namespace TetrisGameTest
{
    [TestClass]
    public class ScoreTest
    {
        private FakeBoard board;

        [TestMethod]
        public void scoreTest()
        {
            Score a = createInstances();

            int expectedPoints = 0;
            int expectedLevel = 0;
            int expectedLines = 0;

            Random rd = new Random();

            for (int i = 1; i < 100; i++)
            {
                int lines = rd.Next(1, 4);

                board.invokeIncrementLine(lines);

                if (lines == 4)
                    expectedPoints += 800;
                else
                    expectedPoints += lines * 100;

                expectedLines += lines;
                expectedLevel = Math.Min((expectedLines / 10 + 1), 10);

                Assert.AreEqual(expectedPoints, a.Points);
                Assert.AreEqual(expectedLevel, a.Level);
                Assert.AreEqual(expectedLines, a.Lines);
            }

            Console.WriteLine("Level: " + expectedLevel + " Lines: " + expectedLines + " Points: " + expectedPoints);
        }

        private Score createInstances()
        {
            FakeBoard board = new FakeBoard(20, 10);
            this.board = board;
            return new Score(board);
        }
    }
}
