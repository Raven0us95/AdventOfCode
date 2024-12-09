using AdventOfCode2023.models.abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Puzzles.day5
{
    internal class PrintQueue : PuzzleBase
    {
        List<string> _rules = new List<string>();
        List<string> _updates = new List<string>();
        List<(int, int)> rules = new List<(int, int)>();
        List<List<int>> updates = new List<List<int>>();
        public PrintQueue(string input, bool isPart2) : base(input, isPart2)
        {
        }
        public override void SolvePart1()
        {
            //Input = GetDefaultInputFromDerived();
            var input = GetStringArray().ToList();

            (rules, updates) = ParseInput(input);

            List<List<int>> correctUpdates = new List<List<int>>();
            foreach (var update in updates)
            {
                if (UpdateIsCorrect(update))
                {
                    correctUpdates.Add(update);
                }
            }

            var resultValues = GetValues(correctUpdates);
            int result = 0;
            foreach (var value in resultValues)
            {
                result += value;
            }
            Console.WriteLine($"The answer is {result}");
        }

        public override void SolvePart2()
        {
            //Input = GetDefaultInputFromDerived();
            var input = GetStringArray().ToList();

            (rules, updates) = ParseInput(input);

            List<List<int>> fixedUpdates = new List<List<int>>();
            foreach (var update in updates)
            {
                if (!UpdateIsCorrect(update))
                {
                    var fixedUpdate = FixUpdate(update);
                    fixedUpdates.Add(fixedUpdate);
                }
            }

            var resultValues = GetValues(fixedUpdates);
            int result = 0;
            foreach (var value in resultValues)
            {
                result += value;
            }
            Console.WriteLine($"The answer is {result}");
        }

        private List<int> FixUpdate(List<int> update)
        {
            var rules = GetMatchingRules(update);

            // We'll keep adjusting the order of update until it satisfies all rules
            bool isSorted = false;
            while (!isSorted)
            {
                isSorted = true;

                // Check each rule and adjust the sequence
                foreach (var rule in rules)
                {
                    int pageA = rule.Item1;
                    int pageB = rule.Item2;

                    // If pageA is found after pageB in the sequence, swap them
                    int indexA = update.IndexOf(pageA);
                    int indexB = update.IndexOf(pageB);

                    if (indexA > indexB)
                    {
                        // Swap pages A and B to respect the rule
                        update[indexA] = pageB;
                        update[indexB] = pageA;

                        // Since we swapped, we need to recheck the sequence
                        isSorted = false;
                    }
                }
            }

            return update;
        }

        private List<int> GetValues(List<List<int>> correctUpdates)
        {
            List<int> values = new List<int>();
            foreach (var update in correctUpdates)
            {
                var x = update[update.Count / 2];
                values.Add(x);
            }
            return values;
        }

        private (List<(int, int)> rules, List<List<int>> updates) ParseInput(List<string> input)
        {
            var emptyLine = input.FirstOrDefault(x => x == "");
            var emptyLineIndex = input.ToList().IndexOf(emptyLine);
            _rules = input.GetRange(0, emptyLineIndex);
            _updates = input.GetRange(emptyLineIndex + 1, input.Count() - emptyLineIndex - 1);

            var rules = _rules.Select(pair =>
            {
                var numbers = pair.Split('|');
                return (int.Parse(numbers[0]), int.Parse(numbers[1]));
            }).ToList();

            var updates = _updates.Select(update =>
            {
                var sequence = update.Split(',');
                return sequence.Select(int.Parse).ToList();
            });

            return (rules, updates.ToList());
        }

        private bool UpdateIsCorrect(List<int> update)
        {
            // filter rules
            var rules = GetMatchingRules(update);

            // Map to store rules as adjacency list
            Dictionary<int, HashSet<int>> ruleMap = new Dictionary<int, HashSet<int>>();

            foreach (var rule in rules)
            {
                if (!ruleMap.ContainsKey(rule.Item1))
                {
                    ruleMap[rule.Item1] = new HashSet<int>();
                }
                ruleMap[rule.Item1].Add(rule.Item2);
            }

            // Set to track seen pages
            HashSet<int> seenPages = new HashSet<int>();

            //// check if this page violates any rule
            foreach (var page in update)
            {
                // check if this page violates any rule
                foreach (var seenPage in seenPages)
                {
                    if (ruleMap.ContainsKey(page) && ruleMap[page].Contains(seenPage))
                    {
                        return false;
                    }
                }

                seenPages.Add(page);
            }

            return true;
        }

        private List<(int, int)> GetMatchingRules(List<int> update)
        {
            var filteredRules = new List<(int, int)>();

            foreach (var rule in rules)
            {
                if (update.Contains(rule.Item1) && update.Contains(rule.Item2)) // &?
                {
                    filteredRules.Add(rule);
                }
            }

            return filteredRules;
        }

        protected override string GetDefaultInputFromDerived()
        {
            return "47|53\r\n97|13\r\n97|61\r\n97|47\r\n75|29\r\n61|13\r\n75|53\r\n29|13\r\n97|29\r\n53|29\r\n61|53\r\n97|53\r\n61|29\r\n47|13\r\n75|47\r\n97|75\r\n47|61\r\n75|61\r\n47|29\r\n75|13\r\n53|13\r\n\r\n75,47,61,53,29\r\n97,61,53,29,13\r\n75,29,13\r\n75,97,47,61,53\r\n61,13,29\r\n97,13,75,29,47";
        }
    }
}
