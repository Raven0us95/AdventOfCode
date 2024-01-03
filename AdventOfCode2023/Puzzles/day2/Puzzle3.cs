using AdventOfCode2023.Factories;
using AdventOfCode2023.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace AdventOfCode2023.Puzzles
{
    public class Puzzle3 : IPuzzle
    {
        public Puzzle3(string input)
        {
            Input = input;
        }
        public string Input { get; set; }
        public CubeGame CurrentGame { get; set; }
        public List<CubeGame> Games { get; set; } = new List<CubeGame>();

        public void Solve()
        {
            int sumOfIds = 0;
            int sumOfCubePower = 0;
            var array = InputFactory.Instance.CreateInputStringArray(Input);
            string testInputPossible = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green";
            string testInputImpossible = "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red";
            //Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
            //Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
            //Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
            //Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
            //Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green

            //check if games are possible if only 12 red, 13 green, 14 blue are in the bag
            //sum the up the game id's of possible games for the solution
            for (int i = 0; i < array.Length; i++)
            {
                CreateGameFromInput(array[i]);
                CurrentGame.PossibilityCheck(12, 13, 14);
                CurrentGame.GetSumOfNeccessaryCubes();
                CurrentGame.EvaluateCubePower();
                Games.Add(CurrentGame);
            }
            
            foreach (var game in Games)
            {
                if (game.IsPossible)
                {
                    sumOfIds += game.Id;
                }

                sumOfCubePower += game.CubePower;
            }
            Console.WriteLine(sumOfCubePower);
            Console.WriteLine(sumOfIds);
        }

        private void CreateGameFromInput(string gameInput)
        {
            var splitGameInfo = gameInput.Split(':');
            var splitSetInfo = splitGameInfo[1].Split(';');

            var removePrefixFromGameId = splitGameInfo[0].Substring(splitGameInfo[0].IndexOf(' ') + 1);

            int.TryParse(removePrefixFromGameId, out int id);
            var sets = splitSetInfo.ToList();
            CurrentGame = new CubeGame(id, sets);
        }
    }
}
