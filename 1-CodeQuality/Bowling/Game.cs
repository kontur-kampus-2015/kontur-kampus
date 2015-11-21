using System;
using System.Collections.Generic;

namespace Bowling
{
    public class Game
    {
        private Dictionary<int, Tuple<int,int?>> _score;
        private int score;
        private int oldScore;
        private int _tmpRoll;
        private bool first;
        public Game()
        {
            _score = new Dictionary<int, Tuple<int, int?>>() ;
            score = 0;
            _tmpRoll = 0;
            first = true;
            oldScore = 0;
        }

        public void Roll(int pins)
        {
            if (first)
            {
                first = false;
                _tmpRoll += 1;
                if (oldScore == 10)
                {
                    score += pins;
                }
                score += pins;
                oldScore = pins;
            }
            else
            {
                first = true;
                if (oldScore + pins == 10)
                {
                    oldScore = 10;
                }
                else
                {
                    oldScore = pins;
                }
                score += pins;

            }
        }

        public int GetScore()
        {
            return score;
        }
    }
}