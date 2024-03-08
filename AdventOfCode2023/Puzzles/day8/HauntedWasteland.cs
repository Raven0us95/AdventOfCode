using AdventOfCode2023.models.abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles.day8
{
    public class HauntedWasteland : PuzzleBase
    {
        private string instructions;
        private int stepCount = 0;
        public HauntedWasteland(string input) : base(input)
        {
        }

        public override void Solve()
        {
            var input = GetInputStringArray();
            instructions = input[0];

            SolvePartOne(input);
            SolvePartTwo(input);
        }

        private void SolvePartTwo(string[] input)
        {
            throw new NotImplementedException();
        }

        private void SolvePartOne(string[] input)
        {
            var currentNode = input.Skip(2).FirstOrDefault(x => x.StartsWith("AAA"));
            int i = 0;
            while (currentNode[0..3] != "ZZZ")
            {
                if (instructions[i] == 'L')
                {
                    currentNode = input.FirstOrDefault(x => x.StartsWith(currentNode[7..10]));
                }
                if (instructions[i] == 'R')
                {
                    currentNode = input.FirstOrDefault(x => x.StartsWith(currentNode[12..15]));
                }
                i++;
                stepCount++;
                if (i == instructions.Length)
                {
                    i = 0;
                }
            }
            Console.WriteLine($"Operation took {stepCount} Steps!");
        }

        

        protected override string GetDefaultInputFromDerived()
        {
            return "LLR\r\n\r\nAAA = (BBB, BBB)\r\nBBB = (AAA, ZZZ)\r\nZZZ = (ZZZ, ZZZ)";
            //"RL\r\n\r\nAAA = (BBB, CCC)\r\nBBB = (DDD, EEE)\r\nCCC = (ZZZ, GGG)\r\nDDD = (DDD, DDD)\r\nEEE = (EEE, EEE)\r\nGGG = (GGG, GGG)\r\nZZZ = (ZZZ, ZZZ)";
        }
    }
}
