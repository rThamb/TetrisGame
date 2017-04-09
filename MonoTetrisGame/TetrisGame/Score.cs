using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    /// Authors: Dimitri Spyropoulos, Renuchan Thambirajah, Hau Gilles Che
    public class Score
    {
        private int level;
        private int lines;
        private int score;

        public Score(IBoard board)
        {
            board.LinesCleared += incrementLinesCleared;
            level = 1; 
        }

        /// <summary>
        /// Access the current level.
        /// </summary>
        public int Level
        {
            get { return level;}
        }

        /// <summary>
        /// Access the total amount of lines cleared.
        /// </summary>
        public int Lines
        {
            get { return lines; }
        }

        /// <summary>
        /// Access the current amount of points.
        /// </summary>
        public int Points
        {
            get { return score; }
        }

        /// <summary>
        /// Handles LinesCleared events and updates the points, level, and lines accordingly.
        /// </summary>
        /// <param name="i">Amount of lines that were cleared.</param>
        private void incrementLinesCleared(int i)
        {
            lines += i;
            if (i == 4)
                score += 800;
            else
                score += (i * 100);
            level = Math.Min((lines / 10 + 1), 10);
        }


    }
}
