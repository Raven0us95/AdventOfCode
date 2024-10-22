using AdventOfCode2023.Handler;
using AdventOfCode2023.models.abstraction;
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles.day10
{
    public class PipeMaze : PuzzleBase
    {
        private bool isEnd = false;
        private bool keepRouting = true;
        private int routingCounter = 0;
        private MazeMap map = new MazeMap();
        public PipeMaze(string input, bool isPart2) : base(input, isPart2)
        {
            
        }

        public override void SolvePart1()
        {
            map.maze = GetInput2DCharArray();

            SetStartPosition(map.maze);

            FindRoute(map.maze);
            Console.WriteLine($"the furthest point from the start is {map.Steps / 2} Steps away!");
        }

        private void FindRoute(char[,] maze)
        {
            while (keepRouting)
            {
                CheckSquarePattern(maze, map.LastPositionY, map.LastPositionX, 1);
                routingCounter++;
                if (routingCounter > 10)
                {
                    SetStartPosition(map.maze);
                    // hit dead End Start next route
                }
            }
        }

        public override void SolvePart2()
        {
            throw new NotImplementedException("there is no Part2 here!");
        }

        private void SetStartPosition(char[,] maze)
        {
            map.Steps = 0;
            routingCounter = 0;
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
                            map.StartPositionX = j;
                            map.StartPositionY = i;
                            map.LastPositionX = map.StartPositionX;
                            map.LastPositionY = map.StartPositionY;
                            map.CurrentNode = currentChar;

                            map.AddNode(j, i);

                            CheckSquarePattern(maze, i, j, 1);
                        }
                    }
                }
            }
        }
        
        private void CheckSquarePattern(char[,] maze, int centerY, int centerX, int radius)
        {
            // Iterate through positions in a square pattern around the center
            for (int i = centerY - radius; i <= centerY + radius; i++)
            {
                for (int j = centerX - radius; j <= centerX + radius; j++)
                {
                    // Check if the current position is within the square pattern
                    if (IsInSquarePattern(centerY, centerX, i, j, radius) &&
                        TwoDimensionalArrayBoundaryHandler.Instance.IsWithinBounds(maze, i, j))
                    {
                        // visualize
                        //Console.WriteLine($"Position in square pattern: ({i}, {j}) - Character: {charArray[i, j]}");
                        //PrintArray(maze, i, j, centerY, centerX);
                        if (IsAdjacent(centerY, centerX, i, j))
                        {
                            map.NextNode = maze[i, j];
                            var canConnect = PipeConnectionHelper.Instance.CanConnect(maze, map.LastPositionY, map.LastPositionX, i, j);
                            if (map.Pipes.Contains(map.NextNode) && canConnect) // add check if pipes can connect
                            {
                                if (map.VisitedNodes.Contains(map.VisitedNodes.FirstOrDefault(x => x.positionX == j && x.positionY == i)))
                                {
                                    if (map.NextNode == 'S')
                                    {
                                        if (isEnd)
                                        {
                                            keepRouting = false;
                                            return;
                                        }
                                        isEnd = true;
                                    }
                                    continue;
                                }
                                map.AddNode(j, i);
                                routingCounter = 0;
                                
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
            // could leave an orange trail for visited nodes
            for (int i = 0; i < charArray.GetLength(0); i++)
            {
                for (int j = 0; j < charArray.GetLength(1); j++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    if (map.Nodes.Any(x => x.Position.positionX == j && x.Position.positionY == i))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    if (i == x && j == y)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    if (i == centerX && j == centerY)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    
                    Console.Write(charArray[i, j]);
                }
                Console.WriteLine();
            }
            Thread.Sleep(100);
        }

        protected override string GetDefaultInputFromDerived()
        {
            return
                "..F7.\r\n" +
                ".FJ|.\r\n" +
                "SJ.L7\r\n" +
                "|F--J\r\n" +
                "LJ...";
                //".....\r\n" +
                //".S-7.\r\n" +
                //".|.|.\r\n" +
                //".L-J.\r\n" +
                //".....";
        }
    }
}
