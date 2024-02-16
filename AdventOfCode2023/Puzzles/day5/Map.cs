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
        public MappingManager MappingManager { get; set; }
        public Map(string name)
        {
            Name = name;
        }
        public void Initialize()
        {
            if (Mappings == null) 
            {
                Console.WriteLine("Mappings not Initialized!");
                return; 
            }
            MappingManager = new MappingManager(Mappings);
        }
        //public long FindSeedLocation(long seed)
        //{ // iterating all mappings takes forever
        //    long location = seed;
        //    foreach (var mapping in Mappings)
        //    {
        //        for (int i = 0; i < mapping.RangeLength; i++)
        //        {
        //            if (seed == mapping.SourceRangeStart + i)
        //            {
        //                location = mapping.DestinationRangeStart + i;
        //                // location found!
        //                return location;
        //            }
        //        }
        //    }
        //    return location;
        //}
    }
    public class MappingManager
    {
        private List<Mapping> sortedMappings;

        public MappingManager(IEnumerable<Mapping> mappings)
        {
            // Sort the mappings based on the SourceRangeStart property
            sortedMappings = mappings.OrderBy(m => m.SourceRangeStart).ToList();
        }

        public long FindSeedLocation(long seed)
        {
            // Perform binary search to find the mapping containing the seed
            int index = BinarySearch(seed);

            if (index >= 0)
            {
                // Calculate the location based on the found mapping
                Mapping mapping = sortedMappings[index];
                long location = mapping.DestinationRangeStart + (seed - mapping.SourceRangeStart);
                return location;
            }
            else
            {
                // Seed not found in any mapping, return the seed itself
                return seed;
            }
        }

        private int BinarySearch(long seed)
        {
            int left = 0;
            int right = sortedMappings.Count - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                Mapping mapping = sortedMappings[mid];

                if (seed >= mapping.SourceRangeStart && seed < mapping.SourceRangeStart + mapping.RangeLength)
                {
                    // Found the mapping containing the seed
                    return mid;
                }
                else if (seed < mapping.SourceRangeStart)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }

            // Seed not found in any mapping
            return -1;
        }
    }


}
