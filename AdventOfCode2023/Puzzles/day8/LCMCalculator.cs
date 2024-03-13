using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles.day8
{
    public class LCMCalculator
    {
        // Method to calculate GCD using Euclidean algorithm
        private long GCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // Method to calculate LCM of two numbers
        private long LCM(long a, long b)
        {
            return (a * b) / GCD(a, b);
        }

        // Method to calculate LCM of an array of numbers
        public long CalculateLCM(long[] numbers)
        {
            if (numbers.Length == 0)
            {
                throw new ArgumentException("Array cannot be empty");
            }

            long lcm = numbers[0];
            for (long i = 1; i < numbers.Length; i++)
            {
                lcm = LCM(lcm, numbers[i]);
            }

            return lcm;
        }
    }
}
