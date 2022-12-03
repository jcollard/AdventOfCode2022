string testSack = "abcdefgh";
Console.WriteLine("Test GetFirst and GetSecond");
Console.WriteLine($"{testSack} => {GetFirst(testSack)} | {GetSecond(testSack)}");
testSack = "DEADBEEF";
Console.WriteLine($"{testSack} => {GetFirst(testSack)} | {GetSecond(testSack)}");
Console.WriteLine("Test Intersection");
testSack = "vJrwpWtwJgWrhcsFMMfFFhFp";
Console.WriteLine($"{testSack} (p) => {FindIntersection(GetFirst(testSack), GetSecond(testSack))}");
testSack = "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL";
Console.WriteLine($"{testSack} (L) => {FindIntersection(GetFirst(testSack), GetSecond(testSack))}");
testSack = "PmmdzqPrVvPwwTWBwg";
Console.WriteLine($"{testSack} (P) => {FindIntersection(GetFirst(testSack), GetSecond(testSack))}");
testSack = "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn";
Console.WriteLine($"{testSack} (v) => {FindIntersection(GetFirst(testSack), GetSecond(testSack))}");
testSack = "ttgJtRGJQctTZtZT";
Console.WriteLine($"{testSack} (t) => {FindIntersection(GetFirst(testSack), GetSecond(testSack))}");
testSack = "CrZsJsPPZsGzwwsLwLmpwMDw";
Console.WriteLine($"{testSack} (s) => {FindIntersection(GetFirst(testSack), GetSecond(testSack))}");
Console.WriteLine("Test GetPriority");
Console.WriteLine($"p (16) => {GetPriority('p')}");
Console.WriteLine($"L (38) => {GetPriority('L')}");
Console.WriteLine($"P (42) => {GetPriority('P')}");
Console.WriteLine($"v (22) => {GetPriority('v')}");
Console.WriteLine($"t (20) => {GetPriority('t')}");
Console.WriteLine($"s (19) => {GetPriority('s')}");

string[] sacks = File.ReadAllLines("sample.txt");
int total = 0;
foreach(string sack in sacks)
{
    string c0 = GetFirst(sack);
    string c1 = GetSecond(sack);
    char item = FindIntersection(c0, c1);
    int priority = GetPriority(item);
    total += priority;
}
Console.WriteLine($"The total priority is: {total}");

string GetFirst(string sack)
{
    return sack.Substring(0, sack.Length/2);
}

string GetSecond(string sack)
{
    return sack.Substring(sack.Length/2, sack.Length/2);
}

char FindIntersection(string first, string second)
{
    return first.Intersect(second).First();
}

int GetPriority(char item)
{
    if (char.IsUpper(item))
    {
        return item - 'A' + 27;
    }
    else
    {
        return item - 'a' + 1;
    }
}