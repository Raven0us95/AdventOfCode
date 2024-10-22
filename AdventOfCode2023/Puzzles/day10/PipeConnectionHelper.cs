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
        static Dictionary<char, List<char>> connectionRules;

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
            connectionRules = new Dictionary<char, List<char>>();

            // Define hardcoded connection rules for each character
            connectionRules['F'] = new List<char> { 'J', '7', 'L', '|', '-', 'S' };
            connectionRules['L'] = new List<char> { 'J', 'F', '|', 'S', '7', '-' };
            connectionRules['J'] = new List<char> { 'F', 'L', '7', '|', 'S', '-' };
            connectionRules['7'] = new List<char> { 'F', 'J', '|', 'L', 'S', '-' };
            connectionRules['|'] = new List<char> { 'F', 'L', 'J', 'S', '|', '7' };
            connectionRules['-'] = new List<char> { 'F', 'J', 'L', 'S', '-', '7' };
            connectionRules['S'] = new List<char> { 'F', 'J', 'L', '7', '|', '-', 'S' }; // S can connect to anything
        }

        // Method to check if two cells can connect based on their characters and positions
        // Method to check if two cells can connect based on their characters
        public bool CanConnect(char[,] maze, int row1, int col1, int row2, int col2)
        {
            // Ensure positions are within bounds
            if (!IsInBounds(maze, row1, col1) || !IsInBounds(maze, row2, col2))
                return false;

            char char1 = maze[row1, col1];
            char char2 = maze[row2, col2];

            if (char1 == '.' || char2 == '.')
            {
                return false;
            }
            // Special case: If either character is 'S', they can always connect
            if (char1 == 'S' || char2 == 'S')
            {
                return true;  // 'S' can always connect to any other character
            }

            // Check the general connection rules
            if (connectionRules.ContainsKey(char1) && connectionRules[char1].Contains(char2))
            {
                // Additional checks for specific directional restrictions
                // Check positions for specific directional rules

                // F can only connect to 7 if it is to the left of 7
                if (char1 == 'F' && char2 == '7' && col1 < col2)
                {
                    return true; // Invalid connection
                }
                // 7 can only connect to F if 7 is to the right of F
                if (char1 == '7' && char2 == 'F' && col1 > col2)
                {
                    return true; // Invalid connection
                }
                // F can only connect to J if F is above J or on the left of J
                if (char1 == 'F' && char2 == 'J' && (row1 < row2 || col1 < col2))
                {
                    return true; // Invalid connection
                }
                // J can only connect to F if F is above J or on the left of F
                if (char1 == 'J' && char2 == 'F' && (row1 > row2 || col1 < col2))
                {
                    return true; // Invalid connection
                }// 7 can only connect to J if 7 is above J
                if (char1 == '7' && char2 == 'J' && (row1 < row2))
                {
                    return true; // Invalid connection
                }
                // F can only connect to - if F is to the left of -
                if (char1 == 'F' && char2 == '-' && col1 < col2)
                {
                    return true; // Invalid connection
                }
                // - can only connect to F if - is to the right of F
                if (char1 == '-' && char2 == 'F' && col1 > col2)
                {
                    return true; // Invalid connection
                }
                // J can only connect to - if J is to the right of -
                if (char1 == 'J' && char2 == '-' && col1 > col2)
                {
                    return true; // Invalid connection
                }
                // - can only connect to J if - is to the left of J
                if (char1 == '-' && char2 == 'J' && col1 < col2)
                {
                    return true; // Invalid connection
                }
                // 7 can only connect to - if 7 is to the right of -
                if (char1 == '7' && char2 == '-' && col1 > col2)
                {
                    return true; // Invalid connection
                }
                // - can only connect to 7 if - is to the left of 7
                if (char1 == '-' && char2 == '7' && col1 < col2)
                {
                    return true; // Invalid connection
                }
                // | can only connect to J if | is above J
                if (char1 == '|' && char2 == 'J' && row1 < row2)
                {
                    return true;
                }
                // J can only connect to | if | is above J
                if (char1 == 'J' && char2 == '|' && row2 < row1)
                {
                    return true; // Invalid connection
                }
                // 7 can only connect to | if 7 is above |
                if (char1 == '7' && char2 == '|' && row1 < row2)
                {
                    return true;
                }
                if (char1 == '|' && char2 == '7' && row1 > row2)
                {
                    return true;
                }
                if (char1 == '|' && char2 == 'L' && row1 < row2)
                {
                    return true;
                }
                if (char1 == 'L' && char2 == '|' && row2 < row1)
                {
                    return true;
                }
                if (char1 == 'L' && char2 == '7' && col1 < col2)
                {
                    return true;
                }
                if (char1 == '7' && char2 == 'L' && col1 > col2)
                {
                    return true;
                }
                if (char1 == 'J' && char2 == '7' && row1 > row2)
                {
                    return true;
                }
                if (char1 == '-' && char2 == '-' && row1 == row2)
                {
                    return true;
                }
                if (char1 == 'J' && char2 == 'L' && col1 > col2)
                {
                    return true;
                }

                return false; // Valid connection found
            }


            return false; // No connection found
        }

        // Helper method to check if a position is within the bounds of the maze
        static bool IsInBounds(char[,] maze, int row, int col)
        {
            return row >= 0 && row < maze.GetLength(0) && col >= 0 && col < maze.GetLength(1);
        }

        // Helper method to determine the relative direction of (row2, col2) from (row1, col1)
        static string GetDirection(int row1, int col1, int row2, int col2)
        {
            if (row1 == row2 && col1 == col2 - 1) return "right";  // right connection
            if (row1 == row2 && col1 == col2 + 1) return "left";   // left connection
            if (row1 == row2 - 1 && col1 == col2) return "down";   // down connection
            if (row1 == row2 + 1 && col1 == col2) return "up";     // up connection
            return null; // no valid connection direction
        }
    }
}
