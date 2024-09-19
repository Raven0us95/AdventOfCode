using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles.day10
{
    public class MazeMap
    {
        public MazeMap()
        {
            Directions.Add("N", "North");
            Directions.Add("NW", "NorthWest");
            Directions.Add("NE", "NorthEast");
            Directions.Add("E", "East");
            Directions.Add("W", "West");
            Directions.Add("S", "South");
            Directions.Add("SE", "SouthEast");
            Directions.Add("SW", "SouthWest");
        }

        public int StartPositionX { get; set; }
        public int StartPositionY { get; set; }
        public int LastPositionX { get; set; }
        public int LastPositionY { get; set; }
        public Dictionary<string, string> Directions { get; set; } = new Dictionary<string, string>();
        public char[] Pipes = new char[] { 'S', 'F', '|', '-', 'L', 'J', '7' };
        public int Steps = 0;
        public string LastDirection = String.Empty;
        public HashSet<char> VisitedNodes = new HashSet<char>();
        public void Move()
        {
            // we have the neccessary information to move
            // how to move in the 2D space?
            
        }
        public string GetMoveDirection(char[,] maze, int currentX, int currentY)
        {
            switch (maze[currentX, currentY])
            {
                case 'F':
                    if (LastPositionY != currentY)
                    {
                        return Directions.FirstOrDefault(x => x.Key == "E").Value;
                    }
                    else
                    {
                        return Directions.FirstOrDefault(x => x.Key == "S").Value;
                    }
                case '|':
                    if (LastPositionY < currentY)
                    {
                        return Directions.FirstOrDefault(x => x.Key == "S").Value;
                    }
                    else
                    {
                        return Directions.FirstOrDefault(x => x.Key == "N").Value;
                    }
                case '-':
                    if (LastPositionX < currentX)
                    {
                        return Directions.FirstOrDefault(x => x.Key == "W").Value;
                    }
                    else
                    {
                        return Directions.FirstOrDefault(x => x.Key == "E").Value;
                    }
                case 'L':
                    if (LastPositionY != currentY)
                    {
                        return Directions.FirstOrDefault(x=>x.Key == "E").Value;
                    }
                    else
                    {
                        return Directions.FirstOrDefault(x => x.Key == "N").Value;
                    }
                case 'J':
                    if (LastPositionY != currentY)
                    {
                        return Directions.FirstOrDefault(x => x.Key == "W").Value;
                    }
                    else
                    {
                        return Directions.FirstOrDefault(x => x.Key == "N").Value;
                    }
                case '7':
                    if (LastPositionY != currentY)
                    {
                        return Directions.FirstOrDefault(x => x.Key == "S").Value;
                    }
                    else
                    {
                        return Directions.FirstOrDefault(x => x.Key == "W").Value;
                    }

                default:
                    return String.Empty;
            }
        }
    }
}
