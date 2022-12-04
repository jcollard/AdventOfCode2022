string testRanges = "2-4,6-8";
Console.WriteLine($"{testRanges} => {ParseRanges(testRanges)}");
testRanges = "2-3,4-5";
Console.WriteLine($"{testRanges} => {ParseRanges(testRanges)}");
testRanges = "27-32,12-42";
Console.WriteLine($"{testRanges} => {ParseRanges(testRanges)}");

(Range, Range) ParseRanges(string ranges)
{
    string[] split = ranges.Split(",");
    return (ParseRange(split[0]), ParseRange(split[1]));
}

Range ParseRange(string range)
{
    string[] bounds = range.Split("-");
    int lower = int.Parse(bounds[0]);
    int upper = int.Parse(bounds[1]);
    return new Range(lower, upper);
}

record Range(int Start, int End);