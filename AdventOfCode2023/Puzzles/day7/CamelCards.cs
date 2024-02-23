using AdventOfCode2023.models.abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles.day7
{
    public class CamelCards : PuzzleBase
    {
        public CamelCards(string input) : base(input)
        {
        }

        public override void Solve()
        {
            var input = GetInputStringArray();
        }

        protected override string GetDefaultInputFromDerived()
        {
            return "";
        }
    }
}
