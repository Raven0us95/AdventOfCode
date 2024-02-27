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
        public int TotalWinnings = 0;

        private void SetHandType(Hand hand, bool useJoker)
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

            if (useJoker)
            {
                // todo Joker mapping
            }
            else
            {
                var group = hand.Cards.GroupBy(c => c);
                if (group.Any(group => group.Count() == 5))
                {// five of a kind
                    hand.Type = handTypes[0];
                    return;
                }
                if (group.Any(group => group.Count() == 4))
                {// four of a kind
                    hand.Type = handTypes[1];
                    return;
                }
                if (group.Any(group => group.Count() == 2) && group.Any(group => group.Count() == 3))
                {// full house
                    hand.Type = handTypes[2];
                    return;
                }
                if (group.Any(group => group.Count() == 3))
                {// three of a kind
                    hand.Type = handTypes[3];
                    return;
                }
                if (group.Count(group => group.Count() == 2) == 2)
                {// two pair
                    hand.Type = handTypes[4];
                    return;
                }
                if (group.Count(group => group.Count() == 2) == 1)
                {// one pair
                    hand.Type = handTypes[5];
                    return;
                }
                // high card
                hand.Type = handTypes[6];
            }
        }
        public void OrderHandsByStrength(bool useJoker)
        {
            // SetHandType
            // lowest to highest Strength (ascending)
            // Order Hands by Type
            // Order Subset of same Types by highest first card in hand, if first cards are the same look for next char..
            foreach (var hand in Hands)
            {
                SetHandType(hand, useJoker);
            }
            if (useJoker)
            {
                // TODO Joker Comparer
            }
            else
            {
                var comparer = new HandComparer(handTypes, cardStrengthOrder);
                Hands = Hands.OrderByDescending(x => x, comparer).ToList();
            }
        }
        public void CalculateTotalWinnings()
        {
            for (int i = 0; i < Hands.Count; i++)
            {
                Hands[i].Rank = i + 1;
                TotalWinnings += Hands[i].Rank * Hands[i].BidAmount;
            }
        }
        public void CreateHandsFromInput(string[] input)
        {
            foreach (var line in input)
            {
                var split = line.Split();
                Hands.Add(new Hand(split[0], int.Parse(split[1])));
            }
        }
    }
    internal class HandComparer : IComparer<Hand>
    {
        private readonly string[] types;
        private readonly char[] customOrder;

        public HandComparer(string[] types, char[] customOrder)
        {
            this.types = types;
            this.customOrder = customOrder;
        }

        public int Compare(Hand x, Hand y)
        {
            // Compare by Hand Type first
            int typeComparison = CompareByHandType(x.Type, y.Type);
            if (typeComparison != 0)
            {
                return typeComparison;
            }
            // if types are equal compare the cards string of each hand based on the customOrder array
            var value = CompareByCards(x.Cards, y.Cards);
            return value;
        }

        private int CompareByCards(string cards1, string cards2)
        {
            for (int i = 0; i < Math.Min(cards1.Length, cards2.Length); i++)
            {
                int indexX = Array.IndexOf(customOrder, cards1[i]);
                int indexY = Array.IndexOf(customOrder, cards2[i]);

                if (indexX != indexY)
                {
                    return indexX.CompareTo(indexY);
                }
            }

            // If the cards strings are equal up to the length of the shortest one, compare their lengths
            var value = cards1.Length.CompareTo(cards2.Length);
            return value;
        }

        private int CompareByHandType(string type1, string type2)
        {
            var indexX = Array.IndexOf(types, type1);
            var indexY = Array.IndexOf(types, type2);
            return indexX.CompareTo(indexY);
        }
    }
}
