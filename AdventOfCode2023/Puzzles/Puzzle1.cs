using AdventOfCode2023.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles
{
    public class Puzzle1
    {
        // On each line, the calibration value can be found by combining the first digit and the last digit(in that order) to form a single two-digit number.
        // Consider your entire calibration document. What is the sum of all of the calibration values?
       
        private string input;
        public Puzzle1(string input)
        {
            this.input = input;
        }
        public void Solve()
        {
            var array = InputFactory.Instance.CreateInputArray(input);
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
    }
}
