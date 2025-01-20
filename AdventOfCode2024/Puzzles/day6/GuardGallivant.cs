using AdventOfCode2023.Helper;
using AdventOfCode2023.models.abstraction;
using shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2024.Puzzles.day6
{
    internal class GuardGallivant : PuzzleBase
    {
        Guard guard = new Guard(new Position(0, 0), "^");
        public GuardGallivant(string input, bool isPart2) : base(input, isPart2)
        {
        }
        public override void SolvePart1()
        {
            //Input = GetDefaultInputFromDerived();
            var input = GetInput2DCharArray();
            var (guardPosition, guardDirection) = FindGuard(input);
            guard.SetPosition(guardPosition, guardDirection);

            // check which indices the guard has to move to
            var map = input;
            bool isMoving = true;
            while (isMoving)
            {
                (map,isMoving) = guard.Move(map);
                //map.PrintArray(guard.Position.Y, guard.Position.X, guard.Position.Y, guard.Position.X, 10, "");
            }

            // count the visited Locations
            int countX = map.Cast<char>().Count(c => c == 'X');
        }

        private (Position Position, string Direction) FindGuard(char[,] input)
        {
            Position position = new Position(0, 0);
            string direction = "";
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    if (input[i, j] == '^' ||
                        input[i, j] == 'v' ||
                        input[i, j] == '<' ||
                        input[i, j] == '>')
                    {
                        position = new Position(j, i);
                        direction = input[i, j].ToString();
                    }
                    //input.PrintArray(i, j, i, j, 10, "");
                }
            }

            var tuple = (Position: position, Direction: direction);
            return tuple;
        }

        public override void SolvePart2()
        {
            throw new NotImplementedException();
        }

        protected override string GetDefaultInputFromDerived()
        {
            return "....#.....\r\n.........#\r\n..........\r\n..#.......\r\n.......#..\r\n..........\r\n.#..^.....\r\n........#.\r\n#.........\r\n......#...";
        }
    }
}
