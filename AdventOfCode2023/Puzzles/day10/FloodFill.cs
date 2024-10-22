using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles.day10
{
    public class FloodFillWorker
    {
        private static FloodFillWorker instance = new FloodFillWorker();
        private FloodFillWorker()
        {
        }
        public static FloodFillWorker Instance => instance;
        public void Work(char[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            // Create a boolean array to mark visited tiles
            bool[,] visited = new bool[rows, cols];

            // Flood fill from the boundary to mark the outside area
            for (int r = 0; r < rows; r++)
            {
                FloodFill(grid, visited, r, 0);        // Left boundary
                FloodFill(grid, visited, r, cols - 1); // Right boundary
            }
            for (int c = 0; c < cols; c++)
            {
                FloodFill(grid, visited, 0, c);        // Top boundary
                FloodFill(grid, visited, rows - 1, c); // Bottom boundary
            }

            // Count the unvisited tiles (these are the enclosed ones)
            int enclosedTileCount = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    // Count unvisited '.' tiles that are within the loop
                    if (grid[r, c] == '0' && !visited[r, c])
                    {
                        enclosedTileCount++;
                    }
                }
            }
            //PrintGrid(grid, visited);
            Console.WriteLine("Enclosed tiles: " + enclosedTileCount);
        }

        // Adjusted Flood fill algorithm to account for special conditions
        static void FloodFill(char[,] grid, bool[,] visited, int row, int col)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            // Base case: check for boundaries and already visited tiles
            if (row < 0 || row >= rows || col < 0 || col >= cols || visited[row, col])
            {
                return;
            }

            // Check for the special conditions where the tiles should not block the flood fill
            if (IsBlockingTile(grid, row, col))
            {
                return;
            }

            // Mark the current tile as visited
            visited[row, col] = true;

            // Recursively fill neighboring tiles
            FloodFill(grid, visited, row + 1, col); // Down
            FloodFill(grid, visited, row - 1, col); // Up
            FloodFill(grid, visited, row, col + 1); // Right
            FloodFill(grid, visited, row, col - 1); // Left
        }

        // Method to check if a tile or sequence of tiles should block the flood fill
        static bool IsBlockingTile(char[,] grid, int row, int col)
        {
            // Handle special cases: adjacent loop segments that do not block the flood fill
            // Example: "||", "J|", "|F", "7F", "JF", "JL", "|L", "7L"
            string[] specialPairs = { 
                "||",
                "J|",
                "|F",
                "7F",
                "JF",
                "JL",
                "|L",
                "7L",
                "7|" };

            // Check for adjacent tiles to determine if this tile should be non-blocking
            if (row > 0)
            {
                string upPair = $"{grid[row, col]}{grid[row - 1, col]}";
                if (Array.IndexOf(specialPairs, upPair) != -1)
                {
                    return false; // It's a special case, treat as non-blocking
                }
            }
            if (col > 0)
            {
                string leftPair = $"{grid[row, col]}{grid[row, col - 1]}";
                if (Array.IndexOf(specialPairs, leftPair) != -1)
                {
                    return false; // It's a special case, treat as non-blocking
                }
            }

            // Define the loop boundary characters
            char[] loopCharacters = { 'S', '7', 'L', 'J', '-', '|', 'F' };

            // Check if the current tile is a loop boundary
            if (Array.IndexOf(loopCharacters, grid[row, col]) != -1)
            {
                return true; // It's a blocking character
            }

            return false; // Not a blocking tile
        }

        static void PrintGrid(char[,] grid, bool[,] visited)
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    if (visited[row, col])
                    {
                        Console.Write("X "); // Mark visited tiles
                    }
                    else
                    {
                        Console.Write(grid[row, col] + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
