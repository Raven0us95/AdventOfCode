using AdventOfCode2024.Puzzles.day1;
using AdventOfCode2024.Puzzles.day3;
using AdventOfCode2024.Puzzles.day5;
using AdventOfCode2024.Puzzles.day6;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        bool repeat = false;
        bool isPart2 = false;
        do
        {
            Console.WriteLine("Please enter the current advent day.");
            var dayInput = Console.ReadLine();
            int.TryParse(dayInput, out int day);
            string path = $@"{AppDomain.CurrentDomain.BaseDirectory}day{day}_input.txt";

            var input = AdventOfCode2023.Factories.InputFactory.Instance.GetInputString(path);

            if (input != null)
            {
                Console.WriteLine("Type 'y' if you want to solve Part2 of the Puzzle");

                var isPart2Input = Console.ReadLine();
                if (isPart2Input != null)
                {
                    if (isPart2Input.FirstOrDefault().ToString().ToLower().Equals("y"))
                    {
                        isPart2 = true;
                    }
                    else
                    {
                        isPart2 = false;
                    }
                }
            }

            switch (day)
            {
                case 1: new Locations(input, isPart2); break;
                case 2: new Reports(input, isPart2); break;
                case 3: new CorruptedData(input, isPart2); break;
                case 4: new CeresSearch(input, isPart2); break;
                case 5: new PrintQueue(input, isPart2); break;
                case 6: new GuardGallivant(input, isPart2); break;
                default:
                    Console.WriteLine("this day is not yet implemented");
                    break;
            }

            Console.WriteLine("Type 'y' if you want to select a new day.");

            var repeatInput = Console.ReadLine();
            if (repeatInput != null)
            {
                if (repeatInput.FirstOrDefault().ToString().ToLower().Equals("y"))
                {
                    repeat = true;
                }
                else { repeat = false; }
            }
        } while (repeat);

        Console.WriteLine("Press any key to exit.");
        Console.ReadLine();
        Environment.Exit(0);
    }
}