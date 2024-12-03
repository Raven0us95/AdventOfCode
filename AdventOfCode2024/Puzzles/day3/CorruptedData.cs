using AdventOfCode2023.models.abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024.Puzzles.day3
{
    public class CorruptedData : PuzzleBase
    {
        public CorruptedData( string input, bool isPart2) : base(input, isPart2)
        {
        }
        public override void SolvePart1()
        {
            Regex regex = new Regex("mul\\((\\d{1,3}),(\\d{1,3})\\)");
            var matches = regex.Matches(Input);
        }

        public override void SolvePart2()
        {
            throw new NotImplementedException();
        }

        protected override string GetDefaultInputFromDerived()
        {
            return "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
        }
    }
}
