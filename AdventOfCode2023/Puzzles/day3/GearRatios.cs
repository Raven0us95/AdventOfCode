using AdventOfCode2023.Factories;
using AdventOfCode2023.Handler;
using AdventOfCode2023.models;
using AdventOfCode2023.models.abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles
{
    public class GearRatios : PuzzleBase
    {
        private int sum = 0;
        private int gearRatioSum = 0;
        private List<int> numbers = new List<int>();
        private List<int> gearRatios = new List<int>();
        private string testInput =
                "467..114..\r\n" +
                "...*......\r\n" +
                "..35..633.\r\n" +
                "......#...\r\n" +
                "617*......\r\n" +
                ".....+.58.\r\n" +
                "..592.....\r\n" +
                "......755.\r\n" +
                "...$.*....\r\n" +
                ".664.598..";

        public GearRatios(string input) : base(input)
        {
            Console.SetWindowSize(20, 15);
            //Console.SetBufferSize(1000, Int16.MaxValue-1);
            Console.CursorVisible = false;
        }

        protected override string GetDefaultInputFromDerived()
        {
            return testInput;
        }

        public override void Solve()
        {
            var schematic = GetInput2DCharArray();

            // find position of the special char
            // check for numeric values around the special char
            // extract the numeric values found (they are enclosed by '.')
            // sum up the extracted values
            for (int i = 0; i < schematic.GetLength(0); i++)
            {
                for (int j = 0; j < schematic.GetLength(1); j++)
                {
                    var currentChar = schematic[i, j];
                    if (!char.IsLetterOrDigit(currentChar) && currentChar != '.')
                    {
                        // special char position found
                        CheckSquarePattern(schematic, i, j, 1, false);

                        // part 2 find the gear ratios of gears (*)
                        if (currentChar == '*')
                        {
                            CheckSquarePattern(schematic, i, j, 1, true);
                        }
                    }
                }
            }
            foreach (var number in numbers)
            {
                sum += number;
            }
            foreach (var gearRatio in gearRatios)
            {
                gearRatioSum += gearRatio;
            }
            Console.Clear();
            Console.WriteLine("Sum of Partnumbers: " + sum);
            Console.WriteLine("Sum of Gearratios: " + gearRatioSum);
        }
        private void CheckSquarePattern(char[,] charArray, int centerX, int centerY, int radius, bool centerIsGear)
        {
            int firstNumber = 0;
            int secondNumber = 0;
            int lastFoundNumber = 0;
            // Iterate through positions in a square pattern around the center
            for (int i = centerX - radius; i <= centerX + radius && (secondNumber == 0 || !centerIsGear); i++)
            {
                for (int j = centerY - radius; j <= centerY + radius && (secondNumber == 0 || !centerIsGear); j++)
                {
                    // Check if the current position is within the square pattern
                    if (IsInSquarePattern(centerX, centerY, i, j, radius) &&
                        TwoDimensionalArrayBoundaryHandler.Instance.IsWithinBounds(charArray, i, j))
                    {
                        // visualize
                        //Console.WriteLine($"Position in square pattern: ({i}, {j}) - Character: {charArray[i, j]}");
                        //PrintArray(charArray, i, j, centerX, centerY);

                        if (char.IsNumber(charArray[i, j]))
                        {
                            // found a number nearby the special character
                            var number = GetFullNumber(charArray, i, j);
                            if (number != lastFoundNumber)
                            {
                                lastFoundNumber = number;
                                if (centerIsGear)
                                {
                                    if (firstNumber == 0)
                                    {
                                        firstNumber = number;
                                    }
                                    else if (secondNumber == 0)
                                    {
                                        secondNumber = number;
                                    }
                                }
                                else
                                {
                                    numbers.Add(number);
                                }
                            }
                        }
                    }
                }
            }
            if (firstNumber * secondNumber != 0 &&
                firstNumber != secondNumber &&
                centerIsGear)
            {
                gearRatios.Add(firstNumber * secondNumber);
            }
        }

        private int GetFullNumber(char[,] charArray, int i, int j)
        {
            StringBuilder number = new StringBuilder();
            // check for digits left to the found number
            for (int k = j; k >= 0 && char.IsDigit(charArray[i, k]); k--)
            {
                number.Insert(0, charArray[i, k]);
            }
            // check for digits right to the found number
            for (int l = j + 1; l < charArray.GetLength(1) && char.IsDigit(charArray[i, l]); l++)
            {
                number.Append(charArray[i, l]);
            }
            int.TryParse(number.ToString(), out int numberValue);
            return numberValue;
        }

        private bool IsInSquarePattern(int centerX, int centerY, int x, int y, int radius)
        {
            // Check if the position (x, y) is within the square pattern around the center (centerX, centerY)
            int deltaX = Math.Abs(x - centerX);
            int deltaY = Math.Abs(y - centerY);

            return deltaX <= radius && deltaY <= radius;
        }

        private bool IsWithinBounds(char[,] charArray, int x, int y)
        {
            // Check if the position (x, y) is within the bounds of the charArray
            var result = x >= 0 && x < charArray.GetLength(0) && y >= 0 && y < charArray.GetLength(1);
            return result;
        }

        private void PrintArray(char[,] charArray, int x, int y, int centerX, int centerY)
        {
            //Print the 2D char array
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < charArray.GetLength(0); i++)
            {
                for (int j = 0; j < charArray.GetLength(1); j++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    if (i == x && j == y)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    if (i == centerX && j == centerY)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write(charArray[i, j]);
                }
                Console.WriteLine();
            }
            Thread.Sleep(1000);
        }
    }
}
