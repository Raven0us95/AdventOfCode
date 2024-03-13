using AdventOfCode2023.models.abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles.day10
{
    public class PipeMaze : PuzzleBase
    {
        public PipeMaze(string input) : base(input)
        {
        }

        public override void Solve()
        {
            var input = GetInputStringArray();
            // GearRatios CheckSquarePattern reusable?
        }

        protected override string GetDefaultInputFromDerived()
        {
            return 
                ".....\r\n" +
                ".S-7.\r\n" +
                ".|.|.\r\n" +
                ".L-J.\r\n" +
                ".....";
        }
    }
}
