using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles.day10
{
    public class FloodFill
    {
        private static FloodFill instance = new FloodFill();
        private FloodFill()
        {
        }
        public static FloodFill Instance => instance;
        
        public void FloodFillArea(char[,] grid, int x, int y, char targetChar, char replacementChar)
        {
            // Base case: return if out of bounds or not the target char
            if (x < 0 || y < 0 || x >= grid.GetLength(0) || y >= grid.GetLength(1))
                return;

            if (grid[x, y] != targetChar)
                return;

            // Replace the character
            grid[x, y] = replacementChar;

            // Recursively call flood fill on adjacent cells
            FloodFillArea(grid, x - 1, y, targetChar, replacementChar); // Up
            FloodFillArea(grid, x + 1, y, targetChar, replacementChar); // Down
            FloodFillArea(grid, x, y - 1, targetChar, replacementChar); // Left
            FloodFillArea(grid, x, y + 1, targetChar, replacementChar); // Right
        }

        // Method to check if a tile or sequence of tiles should block the flood fill

        public void PrintGrid(char[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
