string[] rows = File.ReadAllLines("input.txt");

List<char>[] stacks = ParseStacks(rows);
List<(int amount, int from, int to)> instructions = ParseInstructions(rows);
foreach ((int a, int f, int t) inst in instructions)
{
    MovePart2(inst.a, inst.f, inst.t, stacks);
}
Console.WriteLine(string.Join("",stacks.Select(s => s[0])));

List<(int amount, int from, int to)> ParseInstructions(string[] rows)
{
    List<(int amount, int from, int to)> instructions = new ();
    foreach (string row in rows)
    {
        if (row.Contains("move"))
        {
            string[] inst = row.Replace("move", "").Replace("from", "").Replace("to", "").Replace("  ", " ").Trim().Split();
            int[] inputs = inst.Select(int.Parse).ToArray();
            instructions.Add((inputs[0], inputs[1] - 1, inputs[2] - 1));
        }
    }
    return instructions;
}

void MovePart1(int amount, int from, int to, List<char>[] stacks)
{
    while (amount-- > 0)
    {
        char toMove = stacks[from][0];
        stacks[from].RemoveAt(0);
        stacks[to].Insert(0, toMove);
    }
}

void MovePart2(int amount, int from, int to, List<char>[] stacks)
{
    List<char> chunk = new ();
    while (amount-- > 0)
    {
        char toMove = stacks[from][0];
        stacks[from].RemoveAt(0);
        chunk.Add(toMove);
    }
    chunk.Reverse();
    foreach (char ch in chunk)
    {
        stacks[to].Insert(0, ch);
    }
}

List<char>[] ParseStacks(string[] rows)
{
    int stackCount = (rows[0].Length + 1)/4;
    List<char>[] stacks = InitStacks(stackCount);

    foreach (string row in rows)
    {
        if (!IsStackRow(row)) break;
        ParseStackRow(row, stacks);
    }
    return stacks;
}

void ParseStackRow(string row, List<char>[] stacks)
{
    for (int i = 0; i < stacks.Length; i++)
    {
        int chIx = i*4 + 1;
        char ch = row[chIx];
        if (ch != ' ')
        {
            stacks[i].Add(ch);
        }
    }
}

List<char>[] InitStacks(int count)
{
    List<char>[] stacks = new List<char>[count];
    for (int i = 0; i < stacks.Length; i++)
    {
        stacks[i] = new List<char>();
    }
    return stacks;
}

bool IsStackRow(string row) => row.Contains('[');
