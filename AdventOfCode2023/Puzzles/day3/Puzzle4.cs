using AdventOfCode2023.Helper;
using AdventOfCode2023.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles
{
    public class Puzzle4 : IPuzzle
    {
        private int sum = 0;
        private List<int> numbers = new List<int>();
        public Puzzle4(string input)
        {
            Input = input;
        }
        public string Input { get; set; }

        public void Solve()
        {
            string testInput = 
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
            var schematic = InputFactory.Instance.CreateInput2DCharArray(testInput);

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
                        CheckCircularPattern(schematic, i, j, 1);
                    }
                }
            }
        }
        private void CheckCircularPattern(char[,] charArray, int centerX, int centerY, int radius)
        {
            // Iterate through positions in a square pattern around the center
            for (int i = centerX - radius; i <= centerX + radius; i++)
            {
                for (int j = centerY - radius; j <= centerY + radius; j++)
                {
                    // Check if the current position is within the square pattern
                    if (IsInSquarePattern(centerX, centerY, i, j, radius) &&
                        IsWithinBounds(charArray, i, j))
                    {
                        //Console.WriteLine($"Position in square pattern: ({i}, {j}) - Character: {charArray[i, j]}");
                        PrintArray(charArray, i, j, centerX, centerY);
                        
                        if (char.IsNumber(charArray[i, j]))
                        {
                            // found a number nearby the special character
                            GetFullNumber(charArray, i, j);
                        }
                    }
                }
            }
        }

        private void GetFullNumber(char[,] charArray, int i, int j)
        {
            // extract the numeric values encased by '.'
            // last extracted number should not be the same number
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

        private void PrintArray(char[,]charArray, int x, int y, int centerX, int centerY)
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
