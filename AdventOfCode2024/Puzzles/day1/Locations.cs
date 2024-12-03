using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Puzzles.day1
{
    public class Locations : AdventOfCode2023.models.abstraction.PuzzleBase
    {
        private int LocationID;
        public Locations(string input, bool isPart2) : base(input, isPart2)
        {
        }

        public override void SolvePart1()
        {
            // pair up the numbers and measure how far apart they are.
            // Pair up the smallest number in the left list with the smallest number in the right list,
            // then the second smallest left number with the second smalles right number, and so on

            // within each pair, figure out how far apart the two numbers are
            // you'll need to add up all of those distances

            // find pairs
            // get pair distances
            // add up distances

            //var input = GetDefaultInputFromDerived();
            var split = Input.Split('\n');

            var leftList = getLeftList(split);
            var rightList = getRightList(split);

            var sum = 0;
            for (int i = 0; i < leftList.Count; i++)
            {
                var distance = Math.Abs(leftList[i] - rightList[i]);
                sum += distance;
            }
            Console.WriteLine($"The sum of distances is {sum}");
        }
        public override void SolvePart2()
        {
            var split = Input.Split('\n');
            var leftList = getLeftList(split);
            var rightList = getRightList(split);

            var sum = 0;
            foreach (var item in leftList)
            {
                var duplicates = rightList.Where(x => x == item);
                sum += item * duplicates.Count();
            }

            Console.WriteLine($"the total similarity score is {sum}");
        }

        private List<int> getLeftList(string[] input)
        {
            List<int> output = new List<int>();
            foreach (var item in input)
            {
                var index = item.IndexOf(' ');
                var first = item.Remove(index, item.Length - index).ToString();
                int.TryParse(first, out var firstInt);
                output.Add(firstInt);
            }

            var sorted = output.OrderBy(x => x).ToList();
            return sorted;
        }
        private List<int> getRightList(string[] input)
        {
            List<int> output = new List<int>();
            foreach (var item in input)
            {
                var index = item.LastIndexOf(' ');
                var last = item.Remove(0, index + 1).ToString();
                int.TryParse(last, out var lastInt);
                output.Add(lastInt);
            }
            var sorted = output.OrderBy(x => x).ToList();
            return sorted;
        }

        protected override string GetDefaultInputFromDerived()
        {
            return $"3   4\n4   3\n2   5\n1   3\n3   9\n3   3";
        }
    }
}
