using AdventOfCode2023.Factories;
using AdventOfCode2023.models;
using AdventOfCode2023.models.abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2023.Puzzles.day6
{
    public class ToyRace : PuzzleBase
    {
        private string testInput = "Time:      7  15   30\r\nDistance:  9  40  200";

        public ToyRace(string input, bool isPart2) : base(input, isPart2)
        {
        }

        protected override string GetDefaultInputFromDerived()
        {
            return testInput;
        }

        public override void SolvePart1()
        {
            var input = GetInputStringArray();

            // PART ONE
            int result = 1;
            var races = CreateRaces(input);

            foreach (var race in races)
            {
                race.SetWinningTimes();
                result *= race.WinningTimes.Count();
            }
            //To see how much margin of error you have, determine the number of ways you can beat the record in each race;
            // determine winning button press duration, repeat and add winning durations to array, return array
            // 
            // multiply winning durations
            // sum of multiplication is the solution
            Console.WriteLine(result);
        }

        public override void SolvePart2()
        {
            var input = GetInputStringArray();
            // the input does not describe multiple races, it describes one long race. Ignore the white spaces on the input!
            int result = 1;
            Regex regex = new Regex(@"\b\d+\b");
            var races = new List<Race>();
            var times = regex.Matches(input[0]);
            var distances = regex.Matches(input[1]);

            string time = "";
            string distance = "";
            for (int i = 0; i < times.Count; i++)
            {
                time += times[i].Value;
                distance += distances[i].Value;
            }

            var race = new LongRace(long.Parse(time), long.Parse(distance));
            race.SetWinningTimes();
            Console.WriteLine(race.AmountOfWins);
        }

        private List<Race> CreateRaces(string[] input)
        {
            Regex regex = new Regex(@"\b\d+\b");
            var races = new List<Race>();
            var times = regex.Matches(input[0]);
            var distances = regex.Matches(input[1]);

            for (int i = 0; i < times.Count; i++)
            {
                int.TryParse(times[i].Value, out int time);
                int.TryParse(distances[i].Value, out int distance);
                var race = new Race(time, distance);
                races.Add(race);
            }

            return races;
        }
    }
}
