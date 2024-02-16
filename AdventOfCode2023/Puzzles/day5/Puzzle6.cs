using AdventOfCode2023.Factories;
using AdventOfCode2023.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles.day5
{
    public class Puzzle6 : IPuzzle
    {
        private string testInput = "seeds: 79 14 55 13\r\n\r\nseed-to-soil map:\r\n50 98 2\r\n52 50 48\r\n\r\nsoil-to-fertilizer map:\r\n0 15 37\r\n37 52 2\r\n39 0 15\r\n\r\nfertilizer-to-water map:\r\n49 53 8\r\n0 11 42\r\n42 0 7\r\n57 7 4\r\n\r\nwater-to-light map:\r\n88 18 7\r\n18 25 70\r\n\r\nlight-to-temperature map:\r\n45 77 23\r\n81 45 19\r\n68 64 13\r\n\r\ntemperature-to-humidity map:\r\n0 69 1\r\n1 0 69\r\n\r\nhumidity-to-location map:\r\n60 56 37\r\n56 93 4";
        public Puzzle6(string input)
        {
            Input = input;
        }
        public string Input { get; set; }

        public void Solve()
        {
            string[] input;
            if (Input is null)
            {
                input = InputFactory.Instance.CreateInputStringArray(testInput);
            }
            else
            {
                input = InputFactory.Instance.CreateInputStringArray(Input);
            }
            var seeds = GetSeeds(input);
            var maps = CreateMaps(input);

            List<long> seedLocations = new List<long>();
            foreach (var seed in seeds) // seeds async?
            {
                // using the maps, we need to find the location for each seed
                // the map names tell us what map we have to use next
                // start with seed to soil
                // when the map name contains 'to location' we found the location for the initial seed
                var seedLocation = seed;
                foreach (var map in maps)
                { // atleast in the testInput the maps are in the correct order, so we dont have to look at the names
                    seedLocation = map.FindSeedLocation(seedLocation);
                    if (map.Name == "humidity-to-location map:")
                    {
                        seedLocations.Add(seedLocation);
                        Console.WriteLine($"Location for Seed ({seeds.IndexOf(seed) + 1}/{seeds.Count}) Found!");
                    }
                }
            }
            // find the lowest seedLocation
            Console.WriteLine(seedLocations.Min());
        }

        private List<Map> CreateMaps(string[] input)
        {
            var maps = new List<Map>();
            for (int i = 2; i < input.Length; i++)
            {
                int mapStartIndex = 0;
                if (input[i].Contains("map"))
                {
                    mapStartIndex = i;
                }
                else { continue; }
                var mapEndIndex = Array.IndexOf(input, String.Empty, mapStartIndex); // last map might not end with String.Empty, take Index of input.Length
                if (mapEndIndex < 0) { mapEndIndex= input.Length - 1; }
                var mapInput = CreateMapInput(input, mapStartIndex, mapEndIndex);

                var map = CreateMap(mapInput);
                maps.Add(map);
            }
            
            return maps;
        }

        private List<string> CreateMapInput(string[] input, int mapStartIndex, int mapEndIndex)
        {
            var mapInput = new List<string>();
            for (int i = mapStartIndex; i <= mapEndIndex; i++)
            {
                mapInput.Add(input[i]);
            }
            return mapInput;
        }

        private List<long> GetSeeds(string[] input)
        {
            var seeds = new List<long>();
            var split = input[0].Split(' ');

            for (int i = 1; i < split.Length; i++)
            {
                seeds.Add(long.Parse(split[i]));
            }

            return seeds;
        }

        private Map CreateMap(List<string> input)
        {
            string mapName = input[0];
            var map = new Map(mapName);

            for (int i = 1; i < input.Count; i++)
            {
                var split = input[i].Split(" ");
                if (split[0] == String.Empty)
                {
                    continue;
                }
                long.TryParse(split[1], out long sourceRangeStart);
                long.TryParse(split[0], out long destinationRangeStart);
                long.TryParse(split[2], out long rangeLength);
                var mapping = new Mapping(mapName, destinationRangeStart, sourceRangeStart, rangeLength);
                map.Mappings.Add(mapping);
            }
            
            return map;
        }
    }
}
