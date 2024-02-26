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
        private HandManager handManager = new HandManager();
        public CamelCards(string input) : base(input)
        {
        }

        public override void Solve()
        {
            var input = GetInputStringArray();
            handManager.CreateHandsFromInput(input);
            handManager.OrderHandsByStrength();
            handManager.MultiplyHandsBidByStrength();
            Console.WriteLine($"Total Winnings: {handManager.GetTotalWinnings()}");
        }

        protected override string GetDefaultInputFromDerived()
        {
            return "32T3K 765\r\nT55J5 684\r\nKK677 28\r\nKTJJT 220\r\nQQQJA 483";
        }
    }
}
