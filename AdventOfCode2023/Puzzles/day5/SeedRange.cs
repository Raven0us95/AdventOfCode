namespace AdventOfCode2023.Puzzles.day5
{
    public class SeedRange
    {
        public SeedRange(long v1, long v2)
        {
            Seed = v1;
            Range = v2;
        }

        public long Seed { get; }
        public long Range { get; }
    }
}
