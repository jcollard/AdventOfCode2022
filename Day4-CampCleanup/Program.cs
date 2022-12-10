﻿string testRanges = "2-4,6-8";
Console.WriteLine("Test Parse:");
Console.WriteLine($"{testRanges} => {ParseRanges(testRanges)}");
testRanges = "2-3,4-5";
Console.WriteLine($"{testRanges} => {ParseRanges(testRanges)}");
testRanges = "27-32,12-42";
Console.WriteLine($"{testRanges} => {ParseRanges(testRanges)}");

Console.WriteLine("Test Contain");
Range first = new Range(2, 4);
Range second = new Range(6, 8);
Console.WriteLine($"{first} contains {second}: {Contains(first, second)}");
first = new Range(2, 8);
second = new Range(3, 7);
Console.WriteLine($"{first} contains {second}: {Contains(first, second)}");
first = new Range(6, 6);
second = new Range(4, 6);
Console.WriteLine($"{first} contains {second}: {Contains(first, second)}");

string[] rows = File.ReadAllLines("sample.txt");
int count = 0;
foreach (string row in rows)
{
    (first, second) = ParseRanges(row);
    if (Contains(first, second))
    {
        count++;
    }
}
Console.WriteLine($"Found {count} pairs!");

bool Contains(Range first, Range second)
{
    return (first.Start <= second.Start && first.End >= second.End) ||
            (second.Start <= first.Start && second.End >= first.End);
}

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