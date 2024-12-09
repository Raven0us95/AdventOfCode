using AdventOfCode2023.Handler;
using System.Collections.Generic;

namespace shared
{
    public static class ArrayExtensions
    {
        public static T[] Filter<T>(this T[] array, Func<T, bool> predicate)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return array.Where(predicate).ToArray();
        }
        public static void PrintArray<T>(this T[,] array, int x, int y, int centerX, int centerY, int sleepTime, string additionalLine)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (x < 0 || x > array.GetLength(1) || y < 0 || y > array.GetLength(0) || centerY < 0 || centerY > array.GetLength(0) || centerX < 0 || centerX > array.GetLength(1))
            {
                return;
            }
            //Print the 2D char array
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            // could leave an orange trail for visited nodes
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.ForegroundColor = ConsoleColor.White;

                    if (i == x && j == y)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    if (i == centerX && j == centerY)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }

                    Console.Write(array[i, j]);
                }
                Console.WriteLine();
            }

            Console.WriteLine($"Position: ({x}, {y}) - Character: {array[x, y]} - Last Character {array[centerX, centerY]}");
            Console.WriteLine(additionalLine);
            if (sleepTime > 0)
            {
                Thread.Sleep(sleepTime);
            }
        }

        public static bool IsAdjacent<T>(this T[,] array, int currentPositionX, int currentPositionY, int targetPositionX, int targetPositionY)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            // Check if the target position is vertically or horizontally adjacent to the current position
            return Math.Abs(targetPositionX - currentPositionX) == 1 && targetPositionY == currentPositionY ||
                   Math.Abs(targetPositionY - currentPositionY) == 1 && targetPositionX == currentPositionX;
        }

        public static bool IsInSquarePattern<T>(this T[,] array, int centerX, int centerY, int x, int y, int radius)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            // Check if the position (x, y) is within the square pattern around the center (centerX, centerY)
            int deltaX = Math.Abs(x - centerX);
            int deltaY = Math.Abs(y - centerY);

            return deltaX <= radius && deltaY <= radius;
        }

        public static bool IsWithinBounds<T>(this T[,] array, int x, int y)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            // Check if the position (x, y) is within the bounds of the charArray
            var result = x >= 0 && x < array.GetLength(0) && y >= 0 && y < array.GetLength(1);
            return result;
        }
    }
}
