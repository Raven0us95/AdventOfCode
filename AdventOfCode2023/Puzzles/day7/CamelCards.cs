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
        public CamelCards(string input, bool isPart2) : base(input, isPart2)
        {
        }

        public override void SolvePart1()
        {
            var input = GetStringArray();

            var handManager = new HandManager();
            handManager.CreateHandsFromInput(input);
            handManager.OrderHandsByStrength(false);
            handManager.CalculateTotalWinnings();
            Console.WriteLine($"Total Winnings: {handManager.TotalWinnings}");
        }
        public override void SolvePart2()
        {
            var input = GetStringArray();

            var handManager = new HandManager();
            handManager.CreateHandsFromInput(input);
            handManager.OrderHandsByStrength(true);
            handManager.CalculateTotalWinnings();
            Console.WriteLine($"Total Winnings: {handManager.TotalWinnings}"); // 251385946
        }

        protected override string GetDefaultInputFromDerived()
        {
            return // "32T3K 765\r\nT55J5 684\r\nKK677 28\r\nKTJJT 220\r\nQQQJA 483";
                "AAAAA 2\r\n22222 3\r\nAAAAK 5\r\n22223 7\r\nAAAKJ 6\r\nAAAKK 11\r\n22233 13\r\nAAAKQ 17\r\n22234 19\r\nAAKKQ 23\r\n22334 29\r\nAAKQJ 31\r\n22345 37\r\nAKQJT 41\r\n23456 43";
        }
    }
}
