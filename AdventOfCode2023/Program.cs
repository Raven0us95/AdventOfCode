using AdventOfCode2023.Factories;
using AdventOfCode2023.models;
using AdventOfCode2023.models.abstraction;
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
            int day = 3;
            string path = $@"{AppDomain.CurrentDomain.BaseDirectory}day{day}_input.txt";
            var input = InputFactory.Instance.GetInputString(path);

            //TODO refactor old puzzles
            switch (day)
            {
                case 1: new Trebuchet(input); break;
                case 2: new CubeConundrum(input); break;
                case 3: new GearRatios(input); break;
                case 4: new Puzzle5(input); break;
                case 5: new Puzzle6(input); break;
                case 0:
                default:
                    break;
            }
            //new Puzzle2(input);
            //IPuzzle puzzle3 = new Puzzle3(input);
            //puzzle3.Solve();
            //IPuzzle puzzle4 = new Puzzle4(input);
            //puzzle4.Solve();
            //Puzzle5 puzzle5 = new Puzzle5(input);
            //puzzle5.Solve();
            //Puzzle6 puzzle6 = new Puzzle6(input);
            //puzzle6.Solve();

            //new ToyRace(input);

            Console.ReadLine();
        }
    }
}
