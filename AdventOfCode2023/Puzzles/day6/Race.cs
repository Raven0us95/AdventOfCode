using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles.day6
{
    internal class Race
    {
        public int Time {  get; set; }
        public int RecordDistance { get; set; }
        public Race(int time, int distance)
        {
            Time = time;
            RecordDistance = distance;
        }
        public List<int> WinningTimes { get; } = new List<int>();

        internal void SetWinningTimes()
        {
            // each ms of button press increases velocity by 1mm/ms
            for (int i = 0; i <= Time; i++)
            {
                if (i * (Time - i) > RecordDistance)
                {
                    WinningTimes.Add(i);
                }
            }
        }
    }
}
