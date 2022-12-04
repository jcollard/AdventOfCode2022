string testRanges = "2-4,6-8";
Console.WriteLine($"{testRanges} => {ParseRanges(testRanges)}");
testRanges = "2-3,4-5";
Console.WriteLine($"{testRanges} => {ParseRanges(testRanges)}");
testRanges = "27-32,12-42";
Console.WriteLine($"{testRanges} => {ParseRanges(testRanges)}");

(Range, Range) ParseRanges(string ranges)
{
    return (new Range(0, 0), new Range(0, 0));
}

record Range(int Start, int End);