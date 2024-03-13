using AdventOfCode2023.models.abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

            //SolvePartOne(input);
            SolvePartTwo(input);
        }

        private void SolvePartTwo(string[] input)
        {
            // find starting nodes ending with A
            // start at the same time and find endnodes ending with Z
            //var startingNodes = input.Skip(2).Where(x => x[2] == 'A').ToList();
            //var currentNodes = new HashSet<string>(startingNodes);

            Dictionary<string, (string, string)> nodes = CreateNodes(input);
            Dictionary<string, (string, string)> startingNodes = nodes.Where(x => x.Key.Contains('A')).ToDictionary();
            long steps = 0;
            int i = 0;
            List<string[]> paths = new List<string[]>(); 
            var nodeMapBuilder = new NodeMapBuilder(nodes);

            foreach (var node in startingNodes)
            {
                paths.Add(nodeMapBuilder.BuildPath(instructions.ToCharArray(), node.Key));
            }

            steps = GetResult(paths);
            //Test(nodes);

            //while (!currentNodes.All(node => node[2] == 'Z'))
            //{
            //    var nextNodes = new HashSet<string>();
            //    foreach (var node in currentNodes)
            //    {
            //        if (instructions[i] == 'L')
            //        {
            //            nextNodes.Add(input.FirstOrDefault(x => x.StartsWith(node[7..10])));
            //        }
            //        if (instructions[i] == 'R')
            //        {
            //            nextNodes.Add(input.FirstOrDefault(x => x.StartsWith(node[12..15])));
            //        }
            //    }
            //    currentNodes = nextNodes;
            //    steps++;
            //    i = (i + 1) % instructions.Length;
            //}
            Console.WriteLine($"Operation took {steps} Steps!");
        }

        private long GetResult(List<string[]> paths)
        {
            // das KGV von allen paths
            var counts = new List<long>();
            LCMCalculator calculator = new LCMCalculator();
            foreach (var path in paths)
            {
                counts.Add(path.Count() - 1);
            }
            var lcm = calculator.CalculateLCM(counts.ToArray());
            var result = lcm;
            return result;
        }

        private void Test(Dictionary<string, (string, string)> nodes)
        {
            Dictionary<string, (string,string)> startingNodes = nodes.Where(x => x.Key.Contains('A')).ToDictionary();
            var currentNodes = new HashSet<string>(startingNodes.Keys);
            int i = 0;
            int steps = 0;
            while (!currentNodes.All(node => node[2] == 'Z'))
            {
                var nextNodes = new HashSet<string>();
                foreach (var node in currentNodes)
                {
                    if (instructions[i] == 'L')
                    {
                        nextNodes.Add(nodes.FirstOrDefault(x => x.Key.Equals(node)).Value.Item1);
                    }
                    if (instructions[i] == 'R')
                    {
                        nextNodes.Add(nodes.FirstOrDefault(x => x.Key.Equals(node)).Value.Item2);
                    }
                }
                currentNodes = nextNodes;
                steps++;
                i = (i + 1) % instructions.Length;
            }
            Console.WriteLine($"Operation took {steps} Steps!");
        }

        private Dictionary<string, (string, string)> CreateNodes(string[] input)
        {
            var nodes = new Dictionary<string, (string, string)> { };
            foreach (var node in input[2..])
            {
                nodes.Add(node[0..3], (node[7..10], node[12..15]));
            }
            return nodes;
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
            return "LR\r\n\r\n11A = (11B, XXX)\r\n11B = (XXX, 11Z)\r\n11Z = (11B, XXX)\r\n22A = (22B, XXX)\r\n22B = (22C, 22C)\r\n22C = (22Z, 22Z)\r\n22Z = (22B, 22B)\r\nXXX = (XXX, XXX)";
        }
    }
}
