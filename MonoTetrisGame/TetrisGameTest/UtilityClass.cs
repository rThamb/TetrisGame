using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using TetrisGame; 

namespace TetrisGameTest
{
    public static class ShapeMethods
    {
        public static void DrawShape(this Shape a)
        {

            Block[] blocks = new Block[a.Length];

            for (int i = 0; i < a.Length; i++)
                blocks[i] = a[i]; 

            int[,] numboard = new int[20, 10];


            Console.WriteLine(numboard.GetLength(0) + " " + numboard.GetLength(1));

            for (int i = 0; i < blocks.Length; i++)
            {
                Point yolo = blocks[i].Position;

                numboard[yolo.Y, yolo.X] = 1;
            }

            Console.WriteLine();
            Console.WriteLine("------ Start of the Board ---------------");
            Console.WriteLine();



            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (numboard[i, j] == 0)
                        Console.Write("-");
                    else
                        Console.Write("*");

                }
                Console.Write("|");
                Console.WriteLine();


            }


        }
    }

    public class UtilityClass
    {
        public static void DrawFakeBoardContents(FakeBoard a)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (a.board[i, y] != Color.Transparent)
                        Console.Write("*");

                    else
                        Console.Write("-"); 
                }

                Console.Write("|\n");
            }
        }

    }
}
