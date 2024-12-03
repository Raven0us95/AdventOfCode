using AdventOfCode2023.models.abstraction;

public class Reports : PuzzleBase
{
    public Reports(string? input, bool isPart2) : base(input, isPart2)
    {
    }

    public override void SolvePart1()
    {
        // reports can only increase or decrease by a maximum value of 3
        // reports have to be increasing or decreasing only
        // how many reports are safe?

        //Input = GetDefaultInputFromDerived();
        var reports = Input.Split(Environment.NewLine);
        List<string> safeReports = new List<string>();
        foreach (var report in reports)
        {
            var isSafe = CheckReportSafety(report, false);
            if (isSafe)
            {
                safeReports.Add(report);
            }
        }
        Console.WriteLine($"there are {safeReports.Count()} safe reports");
    }

    private bool CheckReportSafety(string report, bool useDampener)
    {
        if (report.Count() < 2) { return true; }
        var reportValues = report.Split(" ").ToList();
        bool isSafe = true;
        bool dampenerIsUsed = false;
        int dampenedLevelIndex = 0;
        bool isIncreasing = true;
        bool isDecreasing = true;
        for (int i = 1; i < reportValues.Count; i++)
        {
            var currentLevel = int.Parse(reportValues[i]);
            var previousLevel = int.Parse(reportValues[i - 1]);
            if (Math.Abs(currentLevel - previousLevel) <= 3)
            {
                if (previousLevel > currentLevel)
                {
                    isIncreasing = false;
                }
                else if (previousLevel < currentLevel)
                {
                    isDecreasing = false;
                }

                if (previousLevel == currentLevel)
                {
                    if (useDampener && !dampenerIsUsed)
                    {
                        dampenerIsUsed = true;
                        dampenedLevelIndex = i - 1;
                    }
                    else
                    {
                        isSafe = false;
                    }
                }
                if (isIncreasing && isDecreasing || !isIncreasing && !isDecreasing)
                {
                    if (useDampener && !dampenerIsUsed)
                    {
                        dampenerIsUsed = true;
                        dampenedLevelIndex = i - 1;
                    }
                    else
                    {
                        isSafe = false;
                    }
                }
            }
            else
            {
                if (useDampener && !dampenerIsUsed)
                {
                    dampenerIsUsed = true;
                    dampenedLevelIndex = i - 1;
                }
                else
                {
                    isSafe = false;
                }
            }
        }

        if (dampenerIsUsed)
        {
            isIncreasing = true;
            isDecreasing = true;
            isSafe = true;
            reportValues.RemoveAt(dampenedLevelIndex);
            for (int i = 1; i < reportValues.Count; i++)
            {
                var currentLevel = int.Parse(reportValues[i]);
                var previousLevel = int.Parse(reportValues[i - 1]);
                if (Math.Abs(currentLevel - previousLevel) <= 3)
                {
                    if (previousLevel > currentLevel)
                    {
                        isIncreasing = false;
                    }
                    else if (previousLevel < currentLevel)
                    {
                        isDecreasing = false;
                    }

                    if (previousLevel == currentLevel)
                    {
                        isSafe = false;
                    }
                    if (isIncreasing && isDecreasing || !isIncreasing && !isDecreasing)
                    {
                        isSafe = false;
                    }
                }
                else
                {
                    isSafe = false;
                }
            }
        }
        return isSafe;
    }

    public override void SolvePart2()
    {
        //Input = GetDefaultInputFromDerived();
        var reports = Input.Split(Environment.NewLine);
        List<string> safeReports = new List<string>();
        foreach (var report in reports)
        {
            var isSafe = CheckReportSafety(report, true);
            if (isSafe)
            {
                safeReports.Add(report);
            }
        }
        Console.WriteLine($"there are {safeReports.Count()} safe reports");
    }

    protected override string GetDefaultInputFromDerived()
    {
        return "7 6 4 2 1\r\n1 2 7 8 9\r\n9 7 6 2 1\r\n1 3 2 4 5\r\n8 6 4 4 1\r\n1 3 6 7 9";
    }
}