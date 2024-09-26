using AdventOfCode2023.Factories;
using AdventOfCode2023.models.abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles
{
    public class Trebuchet : PuzzleBase
    {
        // On each line, the calibration value can be found by combining the first digit and the last digit(in that order) to form a single two-digit number.
        // Consider your entire calibration document. What is the sum of all of the calibration values?

        public Trebuchet(string input, bool isPart2) : base(input, isPart2)
        {
        }

        public override void SolvePart1()
        {
            var array = GetInputStringArray();
            //string[] array = new string[]{
            //    "1abc2",
            //    "pqr3stu8vwx",
            //    "a1b2c3d4e5f",
            //    "treb7uchet",
            //};
            //In this example, the calibration values of these four lines are 12, 38, 15, and 77. Adding these together produces 142.
            int sum = 0;

            for (int i = 0; i < array.Count(); i++)
            {
                var digits = new String(array[i].Where(x => Char.IsDigit(x)).ToArray());
                var firstDigit = digits.First().ToString();
                var lastDigit = digits.Last().ToString();
                string calibrationValue = firstDigit + lastDigit;
                int.TryParse(calibrationValue, out int value);
                sum += value;
            }
            Console.WriteLine(sum);
        }

        public override void SolvePart2()
        {
            var array = GetInputStringArray();
            //string[] array = new string[]{
            //    "two1nine",
            //    "eightwothree",
            //    "abcone2threexyz",
            //    "xtwone3four",
            //    "4nineeightseven2",
            //    "zoneight234",
            //    "7pqrstsixteen"
            //};
            int sum = 0;
            //Regex regex = new Regex(@"(one|two|three|four|five|six|seven|eight|nine|\d+)");
            //MatchCollection matches = regex.Matches("xtwone3fouroneight");
            // regex matches for each individual number

            for (int i = 0; i < array.Count(); i++)
            {
                var digits = GetValidDigits(array[i]);

                if (digits.Count == 0)
                {
                    Console.WriteLine("Index Out Of Range");
                    return;
                }
                var firstDigit = digits.FirstOrDefault().Value;
                var lastDigit = digits.LastOrDefault().Value;

                if (firstDigit.Length > 1)
                {
                    firstDigit = TranslateTextToDigit(firstDigit);
                }
                if (lastDigit.Length > 1)
                {
                    lastDigit = TranslateTextToDigit(lastDigit);
                }

                var calibrationValue = firstDigit + lastDigit;

                int.TryParse(calibrationValue, out int value);
                sum += value;
            }
            Console.WriteLine(sum);
        }

        private List<KeyValuePair<int, string>> GetValidDigits(string input)
        {
            var digits = new List<KeyValuePair<int, string>>();
            int index = -1;
            string[] validDigits = new string[]
            {
                "one",
                "two",
                "three",
                "four",
                "five",
                "six",
                "seven",
                "eight",
                "nine",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
            };
            if (input is null == false)
            {
                foreach (var validDigit in validDigits)
                {
                    if (input.Contains(validDigit))
                    {
                        while ((index = input.IndexOf(validDigit, index + 1)) != -1)
                        {
                            KeyValuePair<int, string> digit = new KeyValuePair<int, string>(index, validDigit);
                            digits.Add(digit);
                        }
                    }
                }
            }
            var orderedDigits = digits.OrderBy(x => x.Key).ToList();
            return orderedDigits;
        }
        private string TranslateTextToDigit(string text)
        {
            switch (text)
            {
                case "one": return "1";
                case "two": return "2";
                case "three": return "3";
                case "four": return "4";
                case "five": return "5";
                case "six": return "6";
                case "seven": return "7";
                case "eight": return "8";
                case "nine": return "9";
                default:
                    return "0";
            }
        }

        protected override string GetDefaultInputFromDerived()
        {
            throw new NotImplementedException();
        }
    }
}
