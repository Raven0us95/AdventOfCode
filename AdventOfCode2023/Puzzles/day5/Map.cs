using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles.day5
{
    internal class Map
    {
        public string Name { get; set; }
        public List<Mapping> Mappings { get; set; } = new List<Mapping>();
        public Map(string name)
        {
            Name = name;
        }
        public long FindSeedLocation(long seed)
        {
            long location = seed;
            foreach (var mapping in Mappings)
            {
                for (int i = 0; i < mapping.RangeLength; i++)
                {
                    if (seed == mapping.SourceRangeStart + i)
                    {
                        location = mapping.DestinationRangeStart + i;
                        // location found!
                        return location;
                    }
                }
            }
            return location;
        }
    }
}
