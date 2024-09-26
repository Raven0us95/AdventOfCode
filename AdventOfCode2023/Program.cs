using AdventOfCode2023.Factories;
using AdventOfCode2023.models;
using AdventOfCode2023.models.abstraction;
using AdventOfCode2023.Puzzles;
using AdventOfCode2023.Puzzles.day10;
using AdventOfCode2023.Puzzles.day4;
using AdventOfCode2023.Puzzles.day5;
using AdventOfCode2023.Puzzles.day6;
using AdventOfCode2023.Puzzles.day7;
using AdventOfCode2023.Puzzles.day8;
using AdventOfCode2023.Puzzles.day9;
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
            Console.WriteLine("Please enter the current advent day.");
            var dayInput = Console.ReadLine();
            int.TryParse(dayInput, out int day);
            string path = $@"{AppDomain.CurrentDomain.BaseDirectory}day{day}_input.txt";

            var input = InputFactory.Instance.GetInputString(path);

            Console.WriteLine("Type 'y' if you want to solve Part2 of the Puzzle");
            bool isPart2 = false;
            var isPart2Input = Console.ReadLine();
            if (isPart2Input != null) 
            {
                if (isPart2Input.FirstOrDefault().ToString().ToLower().Equals("y"))
                {
                    isPart2 = true;
                }
            }

            switch (day)
            {
                case 1: new Trebuchet(input, isPart2); break;
                case 2: new CubeConundrum(input, isPart2); break;
                case 3: new GearRatios(input, isPart2); break;
                case 4: new Scratchcards(input, isPart2); break;
                case 5: new PlantingSeeds(input, isPart2); break;
                case 6: new ToyRace(input, isPart2); break;
                case 7: new CamelCards(input, isPart2); break;
                case 8: new HauntedWasteland(input, isPart2); break;
                case 9: new MirageMaintenance(input, isPart2); break;
                case 10: new PipeMaze(input, isPart2); break;
                default:
                    break;
            }

            Console.WriteLine("Press any key to close the console...");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}
