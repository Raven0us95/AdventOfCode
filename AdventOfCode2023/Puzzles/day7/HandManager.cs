using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2023.Puzzles.day7
{
    public class HandManager
    {
        public List<Hand> Hands { get; set; } = new List<Hand>();
        private char[] cardStrengthOrder = new[] { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };
        private string[] handTypes = new[] { "FiveOfAKind", "FourOfAKind", "FullHouse", "ThreeOfAKind", "TwoPair", "OnePair", "HighCard" };

        private void SetHandType(Hand hand)
        {
            if (hand is null)
            {
                return;
            }
            // type mapping
            #region info
            // Every hand is exactly one type.From strongest to weakest, they are:

            // Five of a kind, where all five cards have the same label: AAAAA
            // Four of a kind, where four cards have the same label and one card has a different label: AA8AA
            // Full house, where three cards have the same label, and the remaining two cards share a different label: 23332
            // Three of a kind, where three cards have the same label, and the remaining two cards are each different from any other card in the hand: TTT98
            // Two pair, where two cards share one label, two other cards share a second label, and the remaining card has a third label: 23432
            // One pair, where two cards share one label, and the other three cards have a different label from the pair and each other: A23A4
            // High card, where all cards' labels are distinct: 23456
            #endregion

            if (hand.Cards.All(x => x.Equals(x)))
            {// five of a kind
                hand.Type = handTypes[0];
                return;
            }
            // probably foreach card in hand.Cards
            if (true)
            {// four of a kind
                return;
            }
            if (true)
            {// three of a kind
                return;
            }
            if (true)
            {// two pair
                return;
            }
            if (true)
            {// one pair
                return;
            }
            hand.Type = handTypes[6];
        }
        public void OrderHandsByStrength()
        {
            // SetHandType
            // lowest to highest Strength (ascending)
            // Order Cards by Type
            // Order Subset of same Types by highest first card in hand
        }
        public void MultiplyHandsBidByStrength()
        {
            for (int i = 1; i < Hands.Count; i++)
            {
                Hands[i].BidAmount = i * Hands[i].BidAmount;
            }
        }
        public void CreateHandsFromInput(string[] input)
        {
            foreach (var hand in input) 
            { 
                var split = hand.Split();
                Hands.Add(new Hand(split[0], int.Parse(split[1])));
            }
        }
        public int GetTotalWinnings()
        {
            int sum = 0;
            foreach (var hand in Hands)
            {
                sum += hand.BidAmount;
            }
            return sum;
        }
    }
}
