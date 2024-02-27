﻿using AdventOfCode2023.models.abstraction;
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
            
            SolvePartOne();
        }

        private void SolvePartOne()
        {
            handManager.OrderHandsByStrength();
            handManager.CalculateTotalWinnings();
            Console.WriteLine($"Total Winnings: {handManager.TotalWinnings}");
        }

        protected override string GetDefaultInputFromDerived()
        {
            return "AAAAA 2\r\n22222 3\r\nAAAAK 5\r\n22223 7\r\nAAAKK 11\r\n22233 13\r\nAAAKQ 17\r\n22234 19\r\nAAKKQ 23\r\n22334 29\r\nAAKQJ 31\r\n22345 37\r\nAKQJT 41\r\n23456 43";
        }
    }
}
