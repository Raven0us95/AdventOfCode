using AdventOfCode2023.models.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.models
{
    public class Game : IGame
    {
        private int sumOfRed = 0;
        private int sumOfGreen = 0;
        private int sumOfBlue = 0;
        public int Id { get; set; }
        public Game(int id, List<string> sets)
        {
            this.Id = id;
            this.Sets = sets;
        }
        public List<string> Sets { get; set; }
        public bool IsPossible { get; set; } = true;
        public int CubePower { get; set; }

        public void ExtractSetinfos()
        {
            // remove spaghetti
        }
        public void EvaluateCubePower()
        {
            // RedCubes x GreenCubes x BlueCubes
            CubePower = sumOfRed * sumOfGreen * sumOfBlue;
        }
        public void GetSumOfNeccessaryCubes()
        {
            foreach (var set in Sets)
            {
                var setInfo = set.Split(',');
                int highestRedAmount = 0;
                int highestGreenAmount = 0;
                int highestBlueAmount = 0;
                foreach (string cubeInfo in setInfo)
                {
                    var cubeInfoSplit = cubeInfo.Split(' ');
                    int.TryParse(cubeInfoSplit[1], out int amount);
                    var cubeType = cubeInfoSplit[2];

                    if (cubeType == "red")
                    {
                        if (amount > highestRedAmount)
                        {
                            highestRedAmount = amount;
                        }
                    }
                    if (cubeType == "green")
                    {
                        if (amount > highestGreenAmount)
                        {
                            highestGreenAmount = amount;
                        }
                    }
                    if (cubeType == "blue")
                    {
                        if (amount > highestBlueAmount)
                        {
                            highestBlueAmount = amount;
                        }
                    }
                }
                if (highestRedAmount > sumOfRed)
                {
                    sumOfRed = highestRedAmount;
                }
                if (highestGreenAmount > sumOfGreen)
                {
                    sumOfGreen = highestGreenAmount;
                }
                if (highestBlueAmount > sumOfBlue)
                {
                    sumOfBlue = highestBlueAmount;
                }
            }
        }
        public void PossibilityCheck(int maxPossibleRedCubes, int maxPossibleGreenCubes, int maxPossibleBlueCubes)
        {
            foreach (var set in Sets)
            {
                var setInfo = set.Split(',');
                foreach (string cubeInfo in setInfo)
                {
                    var cubeInfoSplit = cubeInfo.Split(' ');
                    int.TryParse(cubeInfoSplit[1], out int amount);
                    var cubeType = cubeInfoSplit[2];
                    if (cubeType == "red" && amount > maxPossibleRedCubes)
                    {
                        IsPossible = false;
                        break;
                    }
                    if (cubeType == "green" && amount > maxPossibleGreenCubes)
                    {
                        IsPossible = false;
                        break;
                    }
                    if (cubeType == "blue" && amount > maxPossibleBlueCubes)
                    {
                        IsPossible = false;
                        break;
                    }
                }
            }
        }
    }
}
