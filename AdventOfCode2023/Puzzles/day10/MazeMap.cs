using AdventOfCode2023.Helper;
using AdventOfCode2023.Puzzles.day5;
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
            Directions.Add("E", "East");
            Directions.Add("W", "West");
            Directions.Add("S", "South");
        }

        public int StartPositionX { get; set; }
        public int StartPositionY { get; set; }
        public int LastPositionX { get; set; }
        public int LastPositionY { get; set; }
        public Dictionary<string, string> Directions { get; set; } = new Dictionary<string, string>();
        public char CurrentNode { get; internal set; }

        public char[] Pipes = new char[] { 'S', 'F', '|', '-', 'L', 'J', '7' };
        public int Steps = 0;
        public string CurrentMoveDirection = String.Empty;
        public char NextNode;
        public HashSet<Position> VisitedNodes = new HashSet<Position>();
        internal char[,] maze = null;
        public List<Node> Nodes = new List<Node>();

        public void AddNode(int positionX, int positionY)
        {
            // we have the neccessary information to move
            // how to move in the 2D space?

            // before we tried looking for the instructions of the next node and then changing LastPosition according to the information...
            // this results in movement to south from '7' even though we are not yet on that node (we have to move east first)
            
            var node = new Node(maze[positionY, positionX], positionX, positionY);

            Nodes.Add(node);
            node.MoveDirection = GetMoveDirection(maze, positionX, positionY);
            if (Nodes.IndexOf(node) >= 1)
            {
                node.PreviousNode = Nodes[Nodes.IndexOf(node) -1];
            }
            if (node.PreviousNode != null)
            {
                node.PreviousNode.NextNode = node;
            }

            VisitedNodes.Add(node.Position);
            LastPositionX = positionX;
            LastPositionY = positionY;
            Steps += 1;
        }

        //public void MoveToNextNode()
        //{
        //    SetLastPosition();
        //    CurrentNode = NextNode;
        //    Steps += 1;
        //    VisitedNodes.Add(CurrentNode);
        //}

        private void SetLastPosition()
        {
            if (String.IsNullOrEmpty(CurrentMoveDirection) == false)
            {
                switch (CurrentMoveDirection)
                {
                    case "North":
                        LastPositionY -= 1; break;
                    case "East":
                        LastPositionX += 1; break;
                    case "West":
                        LastPositionX -= 1; break;
                    case "South":
                        LastPositionY += 1; break;
                    default:
                        throw new Exception("no move direction");
                }
            }
        }

        public string GetMoveDirection(char[,] maze, int currentX, int currentY)
        {
            switch (maze[currentY, currentX])
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
                    if (LastPositionX > currentX)
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
                    if (LastPositionY == currentY)
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
