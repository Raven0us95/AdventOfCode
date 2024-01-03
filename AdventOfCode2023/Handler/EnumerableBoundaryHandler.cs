using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Handler
{
    public class EnumerableBoundaryHandler : IBoundaryHandler
    {
        private static EnumerableBoundaryHandler instance = new EnumerableBoundaryHandler();
        private EnumerableBoundaryHandler()
        {

        }
        public static EnumerableBoundaryHandler Instance => instance;

        public bool IsResponsible(object objectToHandle)
        {
            return objectToHandle is IEnumerable<object>;
        }

        public bool IsWithinBounds(object objectToHandle, int index)
        {
            bool result = false;
            if (objectToHandle is IEnumerable<object> list)
            {
                result = true;
                if (list.Count() <= index)
                {
                    result = false;
                }
            }
            
            return result;
        }
    }
}
