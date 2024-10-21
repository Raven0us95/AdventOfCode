using AdventOfCode2023.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles.day10
{
    public class PipeConnectionHelper
    {
        // Define a dictionary that stores the connectivity rules for each character
        static Dictionary<char, Dictionary<string, List<char>>> connectionRules;

        private static PipeConnectionHelper instance = new PipeConnectionHelper();
        private PipeConnectionHelper()
        {
            InitializeConnectionRules();
        }
        public static PipeConnectionHelper Instance => instance;

        //public static void Main(string[] args)
        //{
        //    // Initialize the connection rules
        //    InitializeConnectionRules();

        //    // Define the maze as a 2D char array
        //    char[,] maze = {
        //    {'.', '.', '.', '.', '.'},
        //    {'.', 'S', '-', '7', '.'},
        //    {'.', '|', '.', '|', '.'},
        //    {'.', 'L', '-', 'J', '.'},
        //    {'.', '.', '.', '.', '.'}
        //};

        //    // Example: Check if (1,1) can connect to (1,2)
        //    bool canConnect = CanConnect(maze, 1, 1, 1, 2);
        //    Console.WriteLine(canConnect);  // Output: True

        //    // Check if (2,1) can connect to (3,1)
        //    canConnect = CanConnect(maze, 2, 1, 3, 1);
        //    Console.WriteLine(canConnect);  // Output: True
        //}

        // Method to initialize the connection rules
        public static void InitializeConnectionRules()
        {
            connectionRules = new Dictionary<char, Dictionary<string, List<char>>>();

            // Define connection rules for '-'
            connectionRules['-'] = new Dictionary<string, List<char>> {
            { "left", new List<char> { '-', '7', 'J' } },
            { "right", new List<char> { '-', 'L', 'S' } }
        };

            // Define connection rules for '|'
            connectionRules['|'] = new Dictionary<string, List<char>> {
            { "up", new List<char> { '|', '7', 'L' } },
            { "down", new List<char> { '|', 'S', 'J' } }
        };

            // Define connection rules for 'L'
            connectionRules['L'] = new Dictionary<string, List<char>> {
            { "up", new List<char> { '-' } },
            { "right", new List<char> { '|' } }
        };

            // Define connection rules for 'J'
            connectionRules['J'] = new Dictionary<string, List<char>> {
            { "up", new List<char> { '|' } },
            { "left", new List<char> { '-' } }
        };

            // Define connection rules for '7'
            connectionRules['7'] = new Dictionary<string, List<char>> {
            { "right", new List<char> { '|' } },
            { "down", new List<char> { '-' } }
        };

            // No need to define special rules for 'S', as it connects to anything
        }

        // Method to check if two cells can connect based on their characters and positions
        public bool CanConnect(char[,] maze, int row1, int col1, int row2, int col2)
        {
            // Ensure positions are within bounds
            if (!IsInBounds(maze, row1, col1) || !IsInBounds(maze, row2, col2))
                return false;

            char char1 = maze[row1, col1];
            char char2 = maze[row2, col2];

            // Special case: If either character is 'S', they can always connect
            if (char1 == 'S' || char2 == 'S')
            {
                return true;  // 'S' can always connect to any other character
            }

            // Determine the relative position of the second cell to the first
            string direction = GetDirection(row1, col1, row2, col2);

            // If there's no valid direction, they can't connect
            if (direction == null)
                return false;

            // Check if the character at (row1, col1) can connect to (row2, col2) in the given direction
            if (connectionRules.ContainsKey(char1) && connectionRules[char1].ContainsKey(direction))
            {
                return connectionRules[char1][direction].Contains(char2);
            }

            return false;
        }

        // Helper method to check if a position is within the bounds of the maze
        static bool IsInBounds(char[,] maze, int row, int col)
        {
            return row >= 0 && row < maze.GetLength(0) && col >= 0 && col < maze.GetLength(1);
        }

        // Helper method to determine the relative direction of (row2, col2) from (row1, col1)
        static string GetDirection(int row1, int col1, int row2, int col2)
        {
            if (row1 == row2 && col1 == col2 - 1) return "right";
            if (row1 == row2 && col1 == col2 + 1) return "left";
            if (row1 == row2 - 1 && col1 == col2) return "down";
            if (row1 == row2 + 1 && col1 == col2) return "up";
            return null;
        }
    }
}
