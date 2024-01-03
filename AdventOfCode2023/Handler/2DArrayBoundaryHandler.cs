using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Handler
{
    public class TwoDimensionalArrayBoundaryHandler : IBoundaryHandler
    {
        private static TwoDimensionalArrayBoundaryHandler instance = new TwoDimensionalArrayBoundaryHandler();
        private TwoDimensionalArrayBoundaryHandler()
        {
        }
        public static TwoDimensionalArrayBoundaryHandler Instance => instance;

        public bool IsResponsible(object objectToHandle)
        {
            bool result = false;
            if (objectToHandle is char[,])
            {
                result = true;
            }
            return result;
        }

        public bool IsWithinBounds(object objectToHandle, int x, int y)
        {
            bool result = false;
            if (objectToHandle is char[,] charArray)
            {
                // Check if the position (x, y) is within the bounds of the charArray
                result = x >= 0 && x < charArray.GetLength(0) && y >= 0 && y < charArray.GetLength(1);
            }
            return result;
        }
    }
}
