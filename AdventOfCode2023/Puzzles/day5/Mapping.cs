using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles.day5
{
    public class Mapping
    {
        public string Name { get; set; }
        public long SourceRangeStart { get; set; }
        public long DestinationRangeStart { get; set; }
        public long RangeLength { get; set; }
        public Mapping(string name, long destinationRangeStart, long sourceRangeStart, long rangeLength)
        {
            Name = name;
            SourceRangeStart = sourceRangeStart;
            DestinationRangeStart = destinationRangeStart;
            RangeLength = rangeLength;
        }
    }
}
