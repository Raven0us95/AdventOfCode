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
            int day = 5;
            string path = $@"{AppDomain.CurrentDomain.BaseDirectory}day{day}_input.txt";
            var input = InputFactory.Instance.GetInputString(path);

            //TODO refactor old puzzles
            switch (day)
            {
                case 1: new Trebuchet(input); break;
                case 2: new CubeConundrum(input); break;
                case 3: new GearRatios(input); break;
                case 4: new Scratchcards(input); break;
                case 5: new PlantingSeeds(input); break;
                case 6: new ToyRace(input); break;
                case 0:
                default:
                    break;
            }

            Console.WriteLine("Press any key to close the console...");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}
