using AdventOfCode2023.Factories;
using AdventOfCode2023.models;
using AdventOfCode2023.models.abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AdventOfCode2023.Puzzles.day5
{
    public class PlantingSeeds : PuzzleBase
    {
        private List<long> seeds;
        private List<Map> maps;
        private List<SeedRange> seedsRange;
        long lowestLocation = long.MaxValue;
        private string testInput = "seeds: 79 14 55 13\r\n\r\nseed-to-soil map:\r\n50 98 2\r\n52 50 48\r\n\r\nsoil-to-fertilizer map:\r\n0 15 37\r\n37 52 2\r\n39 0 15\r\n\r\nfertilizer-to-water map:\r\n49 53 8\r\n0 11 42\r\n42 0 7\r\n57 7 4\r\n\r\nwater-to-light map:\r\n88 18 7\r\n18 25 70\r\n\r\nlight-to-temperature map:\r\n45 77 23\r\n81 45 19\r\n68 64 13\r\n\r\ntemperature-to-humidity map:\r\n0 69 1\r\n1 0 69\r\n\r\nhumidity-to-location map:\r\n60 56 37\r\n56 93 4";


        char[] spaceOrHyphen = [' ', '-'];
        string type = "", destType = "";
        Dictionary<string, string> mappingTypes = [];
        List<(string sourceType, long sourceFrom, long sourceTo, long destOffset)> mappings = [];

        public PlantingSeeds(string input) : base(input)
        {
        }

        protected override string GetDefaultInputFromDerived()
        {
            return testInput;
        }
        public override void Solve()
        {
            SolvePartOne();
            // TODO PartTwo should give the correct answer but will take hours to calculate
            SolvePartTwo();
        }

        private void SolvePartTwo()
        {
            var input = GetInputStringArray();

            foreach (string line in input[2..])
            {
                if (line == "") continue;
                if (line.Contains('-'))
                {
                    string[] words = line.Split(spaceOrHyphen, StringSplitOptions.RemoveEmptyEntries);
                    type = words[0]; destType = words[2];
                    mappingTypes[type] = destType;
                }
                else
                {
                    long[] numbers = Array.ConvertAll(line.Split([' ']), Convert.ToInt64);
                    mappings.Add((type, numbers[1], numbers[1] + numbers[2] - 1, numbers[0] - numbers[1]));
                }
            }
            //seedsRange = GetSeedRanges(input);
            //seeds = GetSeedsFromRange(seedsRange);


            long[] seeds = Array.ConvertAll(input[0].Split(' ')[1..], Convert.ToInt64);


            //maps = CreateMaps(input);

            //Parallel.ForEach(seeds, seed =>
            //{
            //    ExecuteSeedRangeMapping(seed, seedsRange, maps);
            //});

            List<(long from, long to)> ranges = [];
            for (int i = 0; i < seeds.Length; i += 2)
            {
                // Seedrange Start/End
                ranges.Add((seeds[i], seeds[i] + seeds[i + 1] - 1));
            }
            type = "seed";
            do
            {
                ranges = GetDestRanges(ranges.OrderBy(r => r.from).ToList(), [.. mappings.Where(m => m.sourceType == type).OrderBy(m => m.sourceFrom)]);
                type = mappingTypes[type];
            } while (type != "location");
            lowestLocation = Math.Min(lowestLocation, ranges.Min(r => r.from));

            // find the lowest seedLocation
            Console.WriteLine($"{lowestLocation} is the lowest Location found");
        }
        List<(long, long)> GetDestRanges(List<(long, long)> sourceRanges, List<(string sourceType, long sourceFrom, long sourceTo, long destOffset)> mappings)
        {// TODO refactoring: Methode könnte Klassen verwenden
            List<(long, long)> result = [];
            foreach ((long, long) sourceRange in sourceRanges)
            {
                (long from, long to) testRange = sourceRange;
                bool allDone = false;
                do
                {
                    // Find the last mapping where the start is less than the start of the range
                    (string sourceType, long sourceFrom, long sourceTo, long destOffset)
                        = mappings.LastOrDefault(m => m.sourceFrom <= testRange.from && testRange.from <= m.sourceTo, ("", 0, 0, 0));
                    // There aren't any
                    if (sourceType == "")
                    {
                        // Does the end fit in any mappings?
                        (sourceType, sourceFrom, sourceTo, destOffset)
                            = mappings.LastOrDefault(m => m.sourceFrom <= testRange.to && testRange.to <= m.sourceTo, ("", 0, 0, 0));
                        if (sourceType == "")
                        {
                            // If there aren't any, add the whole range and end
                            result.Add(testRange);
                            allDone = true;
                        }
                        else
                        {
                            // Add the start of the range, set the range to the end and continue
                            result.Add((testRange.from, sourceFrom - 1));
                            testRange = (sourceFrom, testRange.to);
                        }
                    }
                    // If the end of the mapping is greater than the end of the range, add the whole range (with offset) and end
                    else if (sourceTo >= testRange.to)
                    {
                        result.Add((testRange.from + destOffset, testRange.to + destOffset));
                        allDone = true;
                    }
                    // Otherwise, add from the start of the range to the end of the mapping (with offsets), set the range start to the mapping end plus one and continue
                    else
                    {
                        //sourceNumber = sourceNumber + destFrom - sourceFrom;
                        result.Add((testRange.from + destOffset, sourceTo + destOffset));
                        testRange = (sourceTo + 1, testRange.to);
                    }
                } while (!allDone);
            }
            return result;
        }

        private List<long> GetSeedsFromRange(List<SeedRange> seedsRange)
        {
            var seeds = new List<long>();
            foreach (var seedRange in seedsRange)
            {
                for (long j = 0; j <= seedRange.Range; j++)
                {// this takes alot of time... faster solution?
                    seeds.Add(seedRange.Seed + j);
                }
            }
            return seeds;
        }

        private void SolvePartOne()
        {
            var input = GetInputStringArray();
            seeds = GetSeeds(input);
            maps = CreateMaps(input);

            Parallel.ForEach(seeds, seed =>
            {
                ExecuteSeedMapping(seed, maps);
            });

            // find the lowest seedLocation
            Console.WriteLine($"{lowestLocation} is the lowest Location found");
        }

        private void ExecuteSeedMapping(long seed, List<Map> maps)
        {
            // using the maps, we need to find the location for each seed
            // the map names tell us what map we have to use next
            // start with seed to soil
            // when the map name contains 'to location' we found the location for the initial seed
            var seedLocation = seed;
            foreach (var map in maps)
            { // atleast in the testInput the maps are in the correct order, so we dont have to look at the names
                map.Initialize();

                seedLocation = map?.MappingManager?.FindSeedLocation(seedLocation) ?? -1;

                if (seedLocation < 0)
                {
                    Console.WriteLine("SeedLocation was not Found!");
                }
                if (map.Name == "humidity-to-location map:")
                {
                    //seedLocations.Add(seedLocation);
                    if (seedLocation < lowestLocation)
                    {
                        lowestLocation = seedLocation;
                        Console.WriteLine($"new lowest location found at {seeds.IndexOf(seed) + 1}/{seeds.Count}");
                    }
                    //Console.WriteLine($"Location for Seed ({seeds.IndexOf(seed) + 1}/{seeds.Count}) Found!");
                }
            }
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
                if (mapEndIndex < 0) { mapEndIndex = input.Length - 1; }
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
        private List<SeedRange> GetSeedRanges(string[] input)
        {
            var seedRanges = new List<SeedRange>();
            var split = input[0].Split(' ');
            for (int i = 1; i < split.Length; i += 2)
            {
                var seedRange = new SeedRange(long.Parse(split[i]), long.Parse(split[i + 1]));
                seedRanges.Add(seedRange);
                //for (long j = 0; j <= seedRange.Range; j++)
                //{// this takes alot of time, but is bearable... faster solution?
                //    seeds.Add(seedRange.Seed + j);
                //}
            }

            return seedRanges;
        }
    }
}
