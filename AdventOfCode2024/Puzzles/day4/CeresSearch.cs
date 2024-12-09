using AdventOfCode2023.Handler;
using AdventOfCode2023.Helper;
using AdventOfCode2023.models.abstraction;
using AdventOfCode2024;
using shared;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;

internal class CeresSearch : PuzzleBase
{
    private readonly List<(int x, int y)> offsets =
        [
            (-1, -1), (-1, 0), (-1, 1),
            (0, -1),            (0, 1),
            (1, -1),  (1, 0),   (1, 1)
        ];
    private Position currentOffset = new Position(0, 0);
    private Position wordOffset = new Position(0, 0);
    private static Word xmas = new Word(new char[] { 'X', 'M', 'A', 'S' }, new Position(0, 0));
    private Word currentWord = new Word(xmas.Letters, new Position(0, 0));
    private List<Word> words = new List<Word>();
    public CeresSearch(string? input, bool isPart2) : base(input, isPart2)
    {
    }

    public override void SolvePart1()
    {
        var grid = GetStringArray();
        // find all X positions
        // search for MAS in all offset directions
        // Count XMAS
        //for (var i = 0; i < grid.GetLength(0); i++)
        //{
        //    for (int j = 0; j < grid.GetLength(1); j++)
        //    {
        //        CheckSquarePattern(grid, i, j, 1, xmas.Letters[0]);
        //        if (new string(currentWord.Found) == "XMAS")
        //        {
        //            words.Add(currentWord);
        //        }
        //    }
        //}

        //Console.WriteLine($"There were {words.Count} XMAS found");


        string target = "XMAS";
        int targetLen = target.Length;
        int rows = grid.Length;
        int cols = grid[0].Length;

        // Define the directions (dx, dy)
        int[,] directions = {
            { 0, 1 },   // Right
            { 0, -1 },  // Left
            { 1, 0 },   // Down
            { -1, 0 },  // Up
            { 1, 1 },   // Down-right
            { 1, -1 },  // Down-left
            { -1, 1 },  // Up-right
            { -1, -1 }  // Up-left
        };

        int totalCount = 0;

        // Function to count occurrences in one direction
        bool WordExists(int startX, int startY, int dx, int dy)
        {
            for (int i = 0; i < targetLen; i++)
            {
                int x = startX + i * dx;
                int y = startY + i * dy;

                if (x < 0 || x >= rows || y < 0 || y >= cols || grid[x][y] != target[i])
                    return false;
            }
            return true;
        }

        // Scan the grid
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                for (int d = 0; d < directions.GetLength(0); d++)
                {
                    int dx = directions[d, 0];
                    int dy = directions[d, 1];

                    //char[,] array = GetInput2DCharArray();
                    //array.PrintArray(x,y, dx, dy, 100, "");

                    if (WordExists(x, y, dx, dy))
                        totalCount++;
                }
            }
        }

        Console.WriteLine($"The word XMAS appears {totalCount} times.");
    }

    public override void SolvePart2()
    {
        //Input = GetDefaultInputFromDerived();
        var grid = GetInput2DCharArray();

        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        int foundCount = 0;
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                int diagonalCount = 0;
                for (int diagonalDirectionIndex = 0; diagonalDirectionIndex < Directions.DiagonalsOnly.Length; diagonalDirectionIndex++)
                {
                    var diagonalDirection = Directions.DiagonalsOnly[diagonalDirectionIndex];
                    var oppositeDirection = diagonalDirection * -1;
                    var startingPoint = (x, y) + oppositeDirection;
                    if (SearchWord(startingPoint, diagonalDirection, "MAS"))
                    {
                        diagonalCount++;
                    }
                }

                if (diagonalCount == 2)
                {
                    foundCount++;
                }
            }
        }
        Console.WriteLine($"The X-MAS pattern appears {foundCount} times.");

        bool SearchWord(Point startingPoint, Point direction, string word)
        {
            for (var characterIndex = 0; characterIndex < word.Length; characterIndex++)
            {
                var position = startingPoint + direction * characterIndex;
                if (position.X < 0 ||
                    position.X >= rows ||
                    position.Y < 0 ||
                    position.Y >= cols ||
                    grid[position.X, position.Y] != word[characterIndex])
                {
                    return false;
                }
            }

            return true;
        }
    }

    


    protected override string GetDefaultInputFromDerived()
    {
        return "MMMSXXMASM\r\nMSAMXMSMSA\r\nAMXSXMAAMM\r\nMSAMASMSMX\r\nXMASAMXAMM\r\nXXAMMXXAMA\r\nSMSMSASXSS\r\nSAXAMASAAA\r\nMAMMMXMMMM\r\nMXMXAXMASX";
    }
    public static class Directions
    {
        public static Point[] WithoutDiagonals { get; } =
        [
            (0, 1),
        (1, 0),
        (0, -1),
        (-1, 0),
    ];

        public static Point[] DiagonalsOnly { get; } =
        [
            (1, 1),
        (-1, 1),
        (1, -1),
        (-1, -1)
        ];

        public static Point[] WithDiagonals { get; } =
        [
            (0, 1),
        (1, 0),
        (0, -1),
        (-1, 0),
        (1, 1),
        (-1, 1),
        (1, -1),
        (-1, -1)
        ];

        public static Point3d[] WithoutDiagonals3d { get; } =
        [
            (1, 0, 0),
            (0, 1, 0),
            (0, 0, 1),
            (-1, 0, 0),
            (0, -1, 0),
            (0, 0, -1),
        ];
    }
    public record struct Point(int X, int Y)
    {
        public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);

        public static Point operator -(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);

        public static Point operator *(Point point, int multiple) => new Point(point.X * multiple, point.Y * multiple);

        public Point Normalize() => new Point(X != 0 ? X / Math.Abs(X) : 0, Y != 0 ? Y / Math.Abs(Y) : 0);

        public static implicit operator Point((int X, int Y) tuple) => new Point(tuple.X, tuple.Y);

        public int ManhattanDistance(Point b) => Math.Abs(X - b.X) + Math.Abs(Y - b.Y);
    }
    public record struct Point3d(int X, int Y, int Z)
    {
        public static Point3d operator +(Point3d a, Point3d b) => new Point3d(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        public static Point3d operator -(Point3d a, Point3d b) => new Point3d(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        public Point3d Normalize() => new Point3d(X != 0 ? X / Math.Abs(X) : 0, Y != 0 ? Y / Math.Abs(Y) : 0, Z != 0 ? Z / Math.Abs(Z) : 0);

        public static implicit operator Point3d((int X, int Y, int Z) tuple) => new Point3d(tuple.X, tuple.Y, tuple.Z);

        public int ManhattanDistance(Point3d b) => Math.Abs(X - b.X) + Math.Abs(Y - b.Y) + Math.Abs(Z - b.Z);
    }
    public void CheckSquarePattern(char[,] array, int centerY, int centerX, int radius, char nextChar)
    {

        //if (new string(currentWord.Found) == "XMAS")
        //{
        //    words.Add(currentWord);
        //    currentWord = new Word(xmas.Letters, new Position(centerX, centerY));
        //    CheckSquarePattern(array, yIterator, xIterator, 1, xmas.Letters[0]);
        //}
        // Iterate through positions in a square pattern around the center
        for (int i = centerY - radius; i <= centerY + radius; i++)
        {
            for (int j = centerX - radius; j <= centerX + radius; j++)
            {
                // Check if the current position is within the square pattern
                if (array.IsInSquarePattern(centerY, centerX, i, j, radius) &&
                    TwoDimensionalArrayBoundaryHandler.Instance.IsWithinBounds(array, i, j))
                {
                    //array.PrintArray(i, j, centerY, centerX);
                    currentOffset = new Position(j - centerX, i - centerY);
                    if (currentOffset.Y == wordOffset.Y && currentOffset.X == wordOffset.X ||
                        wordOffset.X == 0 && wordOffset.Y == 0 &&
                        !currentWord.Found.Contains(array[i, j]))
                    {

                        // visualize
                        array.PrintArray(i, j, centerY, centerX, 0, new string(currentWord.Found));
                        if (nextChar != array[i, j])
                        {
                            //currentWord.Found = new char[currentWord.Found.Length];
                            //currentWord.Found[0] = xmas.Letters[0];
                            wordOffset.X = 0;
                            wordOffset.Y = 0;
                            continue;
                        }

                        // find M in any direction, if M is found keep searching in that direction
                        switch (nextChar)
                        {
                            case 'X':
                                if (array[i, j] == xmas.Letters[0])
                                {
                                    currentWord = new Word(xmas.Letters, new Position(j, i));
                                    currentWord.Found[0] = xmas.Letters[0];
                                    CheckSquarePattern(array, i, j, 1, xmas.Letters[1]);
                                }
                                break;
                            case 'M':
                                if (array[i, j] == xmas.Letters[1])
                                {
                                    currentWord.Found[1] = xmas.Letters[1];
                                    wordOffset = currentOffset;
                                    CheckSquarePattern(array, i, j, 1, xmas.Letters[2]);
                                }

                                break;
                            case 'A':
                                if (array[i, j] == xmas.Letters[2])
                                {
                                    currentWord.Found[2] = xmas.Letters[2];
                                    CheckSquarePattern(array, i, j, 1, xmas.Letters[3]);
                                }

                                break;
                            case 'S':
                                if (array[i, j] == xmas.Letters[3])
                                {
                                    currentWord.Found[3] = xmas.Letters[3];
                                }
                                return;
                            default: Console.WriteLine("!FUCK!"); return;
                        }
                    }
                }
            }
        }
    }
}