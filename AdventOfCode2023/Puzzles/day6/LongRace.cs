using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles.day6
{
    internal class LongRace
    {
        public long Time {  get; set; }
        public long RecordDistance {  get; set; }
        public LongRace(long time, long distance)
        {
            Time = time;
            RecordDistance = distance;
        }

        public long AmountOfWins = 0;
        internal void SetWinningTimes()
        {
            // each ms of button press increases velocity by 1mm/ms
            for (int i = 0; i <= Time; i++)
            {
                if (i * (Time - i) > RecordDistance)
                {
                    AmountOfWins++;
                }
            }
        }
    }
}
