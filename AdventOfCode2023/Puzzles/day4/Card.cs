using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles.day4
{
    internal class Card
    {
        public int Index { get; set; }
        public int CountOfMatches { get; set; }
        public List<int> MyNumbers = null;
        public List<int> WinningNumbers = null;
        public Card(List<int> winningNumbers, List<int> myNumbers, int index, int countOfMatches)
        {
            MyNumbers = myNumbers;
            WinningNumbers = winningNumbers;
            Index = index;
            CountOfMatches = countOfMatches;
        }

        public List<Card> SubCards { get; set; } = new List<Card>();
    }
}
