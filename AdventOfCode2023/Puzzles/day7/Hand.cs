using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles.day7
{
    public class Hand
    {
        public Hand(string cards, int bidAmount)
        {
            Cards = cards;
            BidAmount = bidAmount;
        }
        public int BidAmount { get; set; }
        public string Cards {  get; set; }
        public string Type {  get; set; }
        public int Rank {  get; set; }
    }
}
