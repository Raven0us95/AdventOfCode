using AdventOfCode2023.Helper;
using shared;

namespace AdventOfCode2024.Puzzles.day6
{
    internal class Guard
    {
        private Position nextPosition = new Position(0,0);
        public Guard(Position position, string direction)
        {
            Position = position;
            Direction = direction;
        }
        public Position Position { get; set; }
        public string Direction { get; set; }
        public void SetPosition(Position position, string direction)
        {
            Position = position;
            Direction = direction;
        }
        public (char[,], bool) Move(char[,] currentMap)
        {
            char[,] newMap = (char[,])currentMap.Clone();
            // find nextTile
            var nextTile = FindNextTile(currentMap);
            // move to given Direction until we hit a wall
            if (nextTile != String.Empty)
            {
                if (CanMove(nextTile))
                {
                    newMap[Position.Y, Position.X] = 'X';
                    newMap[nextPosition.Y, nextPosition.X] = char.Parse(Direction);
                    Position = new Position(nextPosition.X, nextPosition.Y);
                    return (map: newMap, isMoving: true);
                }
                else
                {
                    // turn right 90
                    switch (Direction)
                    {
                        case "<":
                            Direction = "^";
                            newMap[Position.Y, Position.X] = char.Parse(Direction);
                            break;
                        case ">":
                            Direction = "v";
                            newMap[Position.Y, Position.X] = char.Parse(Direction);
                            break;
                        case "^":
                            Direction = ">";
                            newMap[Position.Y, Position.X] = char.Parse(Direction);
                            break;
                        case "v":
                            Direction = "<";
                            newMap[Position.Y, Position.X] = char.Parse(Direction);
                            break;
                    }
                    return (map: newMap, isMoving: true);
                }
            }

            // guard will leave the map
            currentMap[Position.Y, Position.X] = 'X';
            return (map: currentMap, isMoving: false);
        }

        private string FindNextTile(char[,] currentMap)
        {
            switch (Direction)
            {
                case "<":
                    if (currentMap.IsWithinBounds(Position.X - 1, Position.Y))
                    {
                        nextPosition.X = Position.X - 1;
                        nextPosition.Y = Position.Y;
                        return currentMap[nextPosition.Y, nextPosition.X].ToString();
                    }
                    else
                    {
                        return String.Empty;
                    }
                case ">":
                    if (currentMap.IsWithinBounds(Position.X + 1, Position.Y))
                    {
                        nextPosition.X = Position.X + 1;
                        nextPosition.Y = Position.Y;
                        return currentMap[nextPosition.Y, nextPosition.X].ToString();
                    }
                    else
                    {
                        return String.Empty;
                    }
                case "^":
                    if (currentMap.IsWithinBounds(Position.X, Position.Y - 1))
                    {
                        nextPosition.X = Position.X;
                        nextPosition.Y = Position.Y - 1;
                        return currentMap[nextPosition.Y, nextPosition.X].ToString();
                    }
                    else
                    {
                        return String.Empty;
                    }
                case "v":
                    if (currentMap.IsWithinBounds(Position.X, Position.Y + 1))
                    {
                        nextPosition.X = Position.X;
                        nextPosition.Y = Position.Y + 1;
                        return currentMap[nextPosition.Y, nextPosition.X].ToString();
                    }
                    else
                    {
                        return String.Empty;
                    }
                default:
                    return String.Empty;
            }
        }

        public bool CanMove(string nextTile)
        {
            bool canMove = true;
            if (nextTile == "#")
            {
                canMove = false;
            }
            return canMove;
        }
    }
}
