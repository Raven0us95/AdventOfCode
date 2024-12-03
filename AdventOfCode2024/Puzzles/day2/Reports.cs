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
            var reportValues = report.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var isSafe = CheckReportSafety(reportValues);
            if (isSafe)
            {
                safeReports.Add(report);
            }
        }
        Console.WriteLine($"there are {safeReports.Count()} safe reports");
    }

    private bool CheckReportSafety(List<int> report)
    {
        bool isSafe = true;
        bool isIncreasing = true;
        bool isDecreasing = true;
        for (int i = 1; i < report.Count; i++)
        {
            var currentLevel = report[i];
            var previousLevel = report[i - 1];
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

        return isSafe;
    }

    public override void SolvePart2()
    {
        //Input = GetDefaultInputFromDerived();
        var reports = Input.Split(Environment.NewLine);
        List<string> safeReports = new List<string>();
        foreach (var report in reports)
        {
            var reportValues = report.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var isSafe = CheckReportSafety(reportValues);
            if (isSafe)
            {
                safeReports.Add(report);
            }
            else
            {
                for (int i = 0; i < reportValues.Count; i++)
                {
                    var reportCopy = reportValues.ToList();
                    reportCopy.RemoveAt(i);
                    if (CheckReportSafety(reportCopy))
                    {
                        safeReports.Add(report);
                        break;
                    }
                }
            }
        }
        Console.WriteLine($"there are {safeReports.Count()} safe reports");
    }

    protected override string GetDefaultInputFromDerived()
    {
        return "7 6 4 2 1\r\n1 2 7 8 9\r\n9 7 6 2 1\r\n1 3 2 4 5\r\n8 6 4 4 1\r\n1 3 6 7 9";
    }
}