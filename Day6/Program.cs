Console.WriteLine("Test Window:");
string input = "abcdefghijklmno";
Console.WriteLine(input);
int start = 0;
Console.WriteLine($"{start} => {Window(input, start)}");
start = 1;
Console.WriteLine($"{start} => {Window(input, start)}");
start = 2;
Console.WriteLine($"{start} => {Window(input, start)}");
start = 3;
Console.WriteLine($"{start} => {Window(input, start)}");

Console.WriteLine("\nTest IsDistinct");
string test = "aabc";
Console.WriteLine($"{test} => {IsDistinct(test)}");
test = "abbc";
Console.WriteLine($"{test} => {IsDistinct(test)}");
test = "abbcc";
Console.WriteLine($"{test} => {IsDistinct(test)}");
test = "abcd";
Console.WriteLine($"{test} => {IsDistinct(test)}");

Console.Clear();
string sample = File.ReadAllText("sample.txt");
for (int i = 0; i < sample.Length - 4; i++)
{
    string window = Window(sample, i);
    if (IsDistinct(window))
    {
        Console.WriteLine($"Found window at: {i + 4}");
        break;
    }
}

bool IsDistinct(string ToCheck)
{
    return ToCheck.Distinct().Count() == ToCheck.Length;
}

string Window(string ToExamine, int Start)
{
    return ToExamine.Substring(start, 4);
}