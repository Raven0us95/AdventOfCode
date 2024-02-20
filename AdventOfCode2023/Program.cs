using AdventOfCode2023.Factories;
using AdventOfCode2023.models;
using AdventOfCode2023.Puzzles;
using AdventOfCode2023.Puzzles.day4;
using AdventOfCode2023.Puzzles.day5;
using AdventOfCode2023.Puzzles.day6;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AdventOfCode2023
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = $@"{AppDomain.CurrentDomain.BaseDirectory}input.txt";
            var input = InputFactory.Instance.GetInputString(path);
            //Puzzle1 puzzle1 = new Puzzle1(input);
            //puzzle1.Solve();
            //Puzzle2 puzzle2 = new Puzzle2(input);
            //puzzle2.Solve();
            //IPuzzle puzzle3 = new Puzzle3(input);
            //puzzle3.Solve();
            //IPuzzle puzzle4 = new Puzzle4(input);
            //puzzle4.Solve();
            //Puzzle5 puzzle5 = new Puzzle5(input);
            //puzzle5.Solve();
            //Puzzle6 puzzle6 = new Puzzle6(input);
            //puzzle6.Solve();
            Puzzle7 puzzle7 = new Puzzle7(input);
            puzzle7.Solve();

            Console.ReadLine();
        }
    }
}
