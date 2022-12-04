
int Part1 = Run(IsContained);
int Part2 = Run(IsOverlapping);
Console.WriteLine(Part1);
Console.WriteLine(Part2);

int Run(Func<(Range, Range), bool> pred)
{
    (Range, Range)[] ranges = File
        .ReadAllLines(args[0])
        .Select(ParseRanges)
        .Where(pred)
        .ToArray();
    return ranges.Length;
}

bool IsContained((Range r0, Range r1) ranges) => ranges.r0.Contains(ranges.r1) || ranges.r1.Contains(ranges.r0);
bool IsOverlapping((Range r0, Range r1) ranges) => ranges.r0.Intersects(ranges.r1);

(Range, Range) ParseRanges(string line)
{
    Range[] split = line.Split(",").Select(ParseRange).ToArray();
    return (split[0], split[1]);
}

Range ParseRange(string toParse)
{
    int[] split = toParse.Split("-").Select(int.Parse).ToArray();
    return new Range(split[0], split[1]);
}

public static class RangeExtensions
{
    public static bool Contains(this Range r0, Range r1) => r0.Start.Value <= r1.Start.Value && r0.End.Value >= r1.End.Value;
    public static bool Contains(this Range r0, int value) => value >= r0.Start.Value && value <= r0.End.Value;
    public static bool Intersects(this Range r0, Range r1) => r0.Contains(r1.Start.Value) || r1.Contains(r0.Start.Value);
}