using AdventOfCode2023.Handler;
using AdventOfCode2023.models.abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles.day10
{
    public class PipeMaze : PuzzleBase
    {
        private MazeMap map = new MazeMap();
        public PipeMaze(string input, bool isPart2) : base(input, isPart2)
        {
        }

        public override void SolvePart1()
        {
            var maze = GetInput2DCharArray();

            CreateMazeMap(maze);
        }

        public override void SolvePart2()
        {
            throw new NotImplementedException("there is no Part2 here!");
        }

        private void CreateMazeMap(char[,] maze)
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    var currentChar = maze[i, j];

                    if (char.IsLetterOrDigit(currentChar) && currentChar != '.')
                    {
                        if (currentChar == map.Pipes[0])
                        {
                            // Starting Position found
                            map.StartPositionX = i;
                            map.StartPositionY = j;
                            map.LastPositionX = map.StartPositionX;
                            map.LastPositionY = map.StartPositionY;
                            map.VisitedNodes.Add(currentChar);
                            CheckSquarePattern(maze, i, j, 1);
                        }
                        else
                        {
                            // pipe char position found
                            // find the next pipe that is closest to the StartPosition starting with East (CheckSquarePattern)
                            // depending on which pipe type is found try to move along the pipes and confirm that it loops back to the StartPosition
                            // -> CheckIsLoop if false continue with the next pipe from StartPosition (South) and so on...
                            // count the steps the loop takes divided by 2 -> farthest position from StartPosition
                            //CheckSquarePattern(maze, i, j, 1);

                            Console.WriteLine($"{map.LastPositionX}, Gabagandalf");
                        }
                    }
                }
            }
        }

        private void CheckSquarePattern(char[,] maze, int centerX, int centerY, int radius)
        {
            // Iterate through positions in a square pattern around the center
            for (int i = centerX - radius; i <= centerX + radius; i++)
            {
                for (int j = centerY - radius; j <= centerY + radius; j++)
                {
                    // Check if the current position is within the square pattern
                    if (IsInSquarePattern(centerX, centerY, i, j, radius) &&
                        TwoDimensionalArrayBoundaryHandler.Instance.IsWithinBounds(maze, i, j))
                    {
                        // visualize
                        //Console.WriteLine($"Position in square pattern: ({i}, {j}) - Character: {charArray[i, j]}");
                        PrintArray(maze, i, j, centerX, centerY);
                        if (IsAdjacent(centerX, centerY, i, j))
                        {
                            var currentChar = maze[i, j];
                            if (map.Pipes.Contains(currentChar))
                            {
                                if (map.VisitedNodes.Contains(currentChar))
                                {
                                    break;
                                }
                                map.VisitedNodes.Add(currentChar);
                                
                                map.LastDirection = map.GetMoveDirection(maze, i, j);
                                return;
                            }
                        }
                    }
                }
            }
        }

        private bool IsAdjacent(int currentPositionX, int currentPositionY, int targetPositionX, int targetPositionY)
        {
            // Check if the target position is adjacent to the current position
            return Math.Abs(targetPositionX - currentPositionX) == 1 && targetPositionY == currentPositionY ||
                   Math.Abs(targetPositionY - currentPositionY) == 1 && targetPositionX == currentPositionX;
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

        protected override string GetDefaultInputFromDerived()
        {
            return
                ".....\r\n" +
                ".S-7.\r\n" +
                ".|.|.\r\n" +
                ".L-J.\r\n" +
                ".....";
        }
    }
}
