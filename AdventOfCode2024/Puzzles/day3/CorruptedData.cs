using AdventOfCode2023.models.abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024.Puzzles.day3
{
    public class CorruptedData : PuzzleBase
    {
        public CorruptedData(string input, bool isPart2) : base(input, isPart2)
        {
        }
        public override void SolvePart1()
        {
            Regex regex = new Regex("mul\\((\\d{1,3}),(\\d{1,3})\\)");
            var matches = regex.Matches(Input).Cast<Match>().ToList();

            var sum = 0;
            foreach (var match in matches)
            {
                int firstValue = int.Parse(match.Groups[1].Value);
                int secondValue = int.Parse(match.Groups[2].Value);
                sum += firstValue * secondValue;
            }
            Console.WriteLine($"solution is {sum}");
        }

        public override void SolvePart2()
        {
            //Input = GetDefaultInputFromDerived();
            Regex mulRegex = new Regex("mul\\((\\d{1,3}),(\\d{1,3})\\)");
            var matches = mulRegex.Matches(Input).Cast<Match>().ToList();
            Regex doRegex = new Regex("do\\(\\)");
            var doMatches = doRegex.Matches(Input).Cast<Match>().ToList();
            Regex dontRegex = new Regex("don't\\(\\)");
            var dontMatches = dontRegex.Matches(Input).Cast<Match>().ToList();

            var sum = 0;
            bool isEnabled = true;
            var nextIndex = 0;

            var doIndicies = doMatches.Select(x => x.Index).ToList();
            var dontIndicies = dontMatches.Select(x => x.Index).ToList();
            var mergedIndicies = doIndicies.Concat(dontIndicies).OrderBy(x => x).ToArray();

            foreach (var match in matches)
            {
                if (nextIndex < mergedIndicies.Length && match.Index > mergedIndicies[nextIndex])
                {
                    isEnabled = doIndicies.Contains(mergedIndicies[nextIndex]);
                    nextIndex++;
                }

                if (isEnabled)
                {
                    int firstValue = int.Parse(match.Groups[1].Value);
                    int secondValue = int.Parse(match.Groups[2].Value);
                    sum += firstValue * secondValue;
                }
            }

            Console.WriteLine($"Solution is {sum}");
        }

        protected override string GetDefaultInputFromDerived()
        {
            return "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
        }
    }
}
