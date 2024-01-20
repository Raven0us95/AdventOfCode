using AdventOfCode2023.Factories;
using AdventOfCode2023.Handler;
using AdventOfCode2023.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles.day4
{
    public class Puzzle5 : IPuzzle
    {
        private static readonly object listLock = new object();
        private int sumPart1 = 0;
        private int sumPart2 = 0;
        public string Input { get; set; }
        private string testInput =
                "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53\r\n" +
                "Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19\r\n" +
                "Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1\r\n" +
                "Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83\r\n" +
                "Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36\r\n" +
                "Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11";
        List<string> inputCopy;
        private List<Card> cards = new List<Card>();
        private List<Card> cardInfos = new List<Card>();
        public Puzzle5(string input)
        {
            Input = input;
        }

        public async void Solve()
        {
            string[] input;
            if (Input is null)
            {
                input = InputFactory.Instance.CreateInputStringArray(testInput);
            }
            else
            {
                input = InputFactory.Instance.CreateInputStringArray(Input);
            }

            //part one
            // get count of matching numbers
            // multiply for each match
            for (int i = 0; i < input.Length; i++)
            {
                int countOfMatches = GetCountOfMatches(input[i]);
                int multiplyValue = GetMultiplyValue(countOfMatches);
                sumPart1 += multiplyValue;
            }

            // part two
            // each card gives you copies of the next X = countOfMatches cards
            // insert copies
            // repeat until no more cards are won and all original cards are evaluated
            // sum = amountOfCards
            inputCopy = input.ToList();
            List<Task> cardTasks = new List<Task>();
            for (int i = 0; i < input.Length; i++)
            {
                int index = i;
                // create Cards
                // wenn eine Card evaluiert wurde speichere die Informationen in einer Liste
                // prüfe ob die zu evaluierende Card bereits in der Liste ist
                // wenn ja muss keine neue Card erzeugt werden sondern die Informationen können dem Speicher entnommen werden
                //cards.Add(CreateCard(input[i], i));
                Task createCards = Task.Run(async () => 
                { 
                    var cardResult = await CreateCardAsync(input[index], index);
                    cards.Add(cardResult);
                });
                
                cardTasks.Add(createCards);
                //var card = await CreateCardAsync(input[i], i);
                //cards.Add(card);
                Console.WriteLine($"card {i + 1} of {input.Length} created");
            }
            await Task.WhenAll(cardTasks);
            foreach (var card in cards)
            {
                sumPart2 += 1;
                sumPart2 += GetSumOfCard(card);
            }
            Console.WriteLine(sumPart2);
            // answer 7013204
        }

        private async Task<Card> CreateCardAsync(string input, int index)
        {
            lock (listLock)
            {
                if (cardInfos.Exists(x => x.Index == index))
                {
                    return cardInfos.FirstOrDefault(x => x.Index == index);
                }
            }
            
            var splitCards = SplitCards(input);
            var winningNumbers = GetNumberList(splitCards[0].Split(' '));
            var myNumbers = GetNumberList(splitCards[1].Split(' '));
            int countOfMatches = GetCountOfMatches(winningNumbers, myNumbers);
            Card card = new Card(winningNumbers, myNumbers, index, countOfMatches);
            if (countOfMatches > 0)
            {
                card.SubCards = await CreateSubCardsAsync(card);
            }
            lock (listLock)
            {
                cardInfos.Add(card);
            }
            
            return card;
        }
        private async Task<List<Card>> CreateSubCardsAsync(Card card)
        {
            List<Task<Card>> cardTasks = new List<Task<Card>>();
            var subCards = new List<Card>();
            if (card.CountOfMatches < 1)
            {
                Console.WriteLine("No more subcards to add");
                return subCards;
            }
            // insert copies for each match
            for (int i = 1; i <= card.CountOfMatches; i++)
            {
                int subCardIndex = card.Index + i;
                if (EnumerableBoundaryHandler.Instance.IsWithinBounds(inputCopy, subCardIndex))
                {
                    cardTasks.Add(CreateCardAsync(inputCopy[subCardIndex], subCardIndex));
                    //Console.WriteLine($"Subcard {i} of {card.CountOfMatches} creation started");
                }
                else
                {
                    Console.WriteLine($"{nameof(CreateSubCardsAsync)} IndexOutOfBounds!");
                }
            }

            var subCardResults = await Task.WhenAll(cardTasks);

            subCards = subCardResults.ToList();
            return subCards;
        }

        private int GetSumOfCard(Card card)
        {
            int sum = 0;
            sum += card.CountOfMatches;
            sum += GetSumOfSubCards(card.SubCards);
            return sum;
        }

        private int GetSumOfSubCards(List<Card> subCards)
        {
            int sum = 0;
            foreach (var card in subCards)
            {
                sum += GetSumOfCard(card);
            }
            return sum;
        }

        private Card CreateCard(string input, int index)
        {
            if (cardInfos.Exists(x => x.Index == index))
            {
                return cardInfos.FirstOrDefault(x => x.Index == index);
            }
            var splitCards = SplitCards(input);
            var winningNumbers = GetNumberList(splitCards[0].Split(' '));
            var myNumbers = GetNumberList(splitCards[1].Split(' '));
            int countOfMatches = GetCountOfMatches(winningNumbers, myNumbers);
            Card card = new Card(winningNumbers, myNumbers, index, countOfMatches);
            if (countOfMatches > 0)
            {
                card.SubCards = CreateSubCards(card);
            }
            cardInfos.Add(card);
            return card;
        }
        private List<Card> CreateSubCards(Card card)
        {
            var subCards = new List<Card>();
            if (card.CountOfMatches < 1)
            {
                Console.WriteLine("No more subcards to add");
                return subCards;
            }
            // insert copies for each match
            for (int i = 1; i <= card.CountOfMatches; i++)
            {
                int subCardIndex = card.Index + i;
                if (EnumerableBoundaryHandler.Instance.IsWithinBounds(inputCopy, subCardIndex))
                {
                    var subCard = CreateCard(inputCopy[subCardIndex], subCardIndex);
                    subCards.Add(subCard);
                }
                else
                {
                    Console.WriteLine($"{nameof(CreateSubCards)} IndexOutOfBounds!");
                }
            }
            return subCards;
        }
        private string[] SplitCards(string input)
        {
            var prefixIndex = input.IndexOf(':');
            var numbersOnlyInput = input.Remove(0, prefixIndex + 1);
            var splitCards = numbersOnlyInput.Split('|');
            return splitCards;
        }

        private int GetMultiplyValue(int countOfMatches)
        {
            if (countOfMatches == 0)
            {
                return 0;
            }
            int result = 1;
            for (int i = 1; i < countOfMatches; i++)
            {
                result = result * 2;
            }
            return result;
        }

        private int GetCountOfMatches(string input)
        {
            int countOfMatches = 0;
            var prefixIndex = input.IndexOf(':');
            var numbersOnlyInput = input.Remove(0, prefixIndex + 1);
            var splitCards = numbersOnlyInput.Split('|');
            var winningNumbers = GetNumberList(splitCards[0].Split(' '));
            var myNumbers = GetNumberList(splitCards[1].Split(' '));

            foreach (var number in winningNumbers)
            {
                for (int i = 0; i < myNumbers.Count; i++)
                {
                    if (number == myNumbers[i])
                    {
                        countOfMatches++;
                    }
                }
            }

            return countOfMatches;
        }
        private int GetCountOfMatches(List<int> winningNumbers, List<int> myNumbers)
        {
            int countOfMatches = 0;
            foreach (var number in winningNumbers)
            {
                for (int i = 0; i < myNumbers.Count; i++)
                {
                    if (number == myNumbers[i])
                    {
                        countOfMatches++;
                    }
                }
            }

            return countOfMatches;
        }

        private List<int> GetNumberList(string[] strings)
        {
            var list = new List<int>();
            foreach (string s in strings)
            {
                if (s == String.Empty == false)
                {
                    int.TryParse(s, out int output);
                    list.Add(output);
                }
            }
            return list;
        }
    }
}
