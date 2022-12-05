
List<List<char>> stacks = InitStacks(3);

Console.WriteLine("\nTest GetNumberOfContainers");
string row = "    [D]    ";
Console.WriteLine($"{row} (3) => {GetNumberOfContainers(row)}");
row = "[Z] [M] [P] [Q]";
Console.WriteLine($"{row} (4) => {GetNumberOfContainers(row)}");
row = "[Z] [M] [P] [Q] [Z]";
Console.WriteLine($"{row} (5) => {GetNumberOfContainers(row)}");

row = "    [D]    ";
Console.WriteLine("Test ParseRow:");
ParseContainerRow(row, stacks);
Console.WriteLine(row);
PrintStacks(stacks);row = "[N] [C]    ";
Console.WriteLine(row);
ParseContainerRow(row, stacks);
PrintStacks(stacks);
row = "[Z] [M] [P]";
Console.WriteLine(row);
ParseContainerRow(row, stacks);
PrintStacks(stacks);

Console.WriteLine("\nTest Move:");
Move(2, 1, stacks);
Console.WriteLine("Move 2 => 1");
PrintStacks(stacks);
Move(1, 2, stacks);
Console.WriteLine("Move 1 => 2");
PrintStacks(stacks);
Move(1, 3, stacks);
Console.WriteLine("Move 1 => 3");
PrintStacks(stacks);
Move(3, 1, stacks);
Console.WriteLine("Move 3 => 1");
PrintStacks(stacks);

Console.WriteLine("\nTest Perform Instruction:");
Instruction instruction = new Instruction(1, 2, 1);
Console.WriteLine(instruction);
PerformInstruction(instruction, stacks);
PrintStacks(stacks);
instruction = new Instruction(3, 1, 3);
Console.WriteLine(instruction);
PerformInstruction(instruction, stacks);
PrintStacks(stacks);
instruction = new Instruction(2, 2, 1);
Console.WriteLine(instruction);
PerformInstruction(instruction, stacks);
PrintStacks(stacks);
instruction = new Instruction(1, 1, 2);
Console.WriteLine(instruction);
PerformInstruction(instruction, stacks);
PrintStacks(stacks);

Console.WriteLine("\nTest ParseInstruction");
row = "move 1 from 2 to 1";
instruction = ParseInstruction(row);
Console.WriteLine($"{row} => {instruction}");
row = "move 3 from 1 to 3";
instruction = ParseInstruction(row);
Console.WriteLine($"{row} => {instruction}");
row = "move 2 from 2 to 1";
instruction = ParseInstruction(row);
Console.WriteLine($"{row} => {instruction}");
row = "move 1 from 1 to 2";
instruction = ParseInstruction(row);
Console.WriteLine($"{row} => {instruction}");

Console.WriteLine("\nTest IsContainerRow");
row = "move 1 from 2 to 1";
Console.WriteLine($"{row} - Container? {IsContainerRow(row)} - Instruction? {IsInstructionRow(row)}");
row = " 1   2   3 ";
Console.WriteLine($"{row} - Container? {IsContainerRow(row)} - Instruction? {IsInstructionRow(row)}");
row = "[Z] [M] [P]";
Console.WriteLine($"{row} - Container? {IsContainerRow(row)} - Instruction? {IsInstructionRow(row)}");

string[] rows = File.ReadAllLines("sample.txt");
int count = GetNumberOfContainers(rows[0]);
List<List<char>> supplyStacks = ParseSupplyStacks(rows);
List<Instruction> instructions = ParseInstructions(rows);
foreach (Instruction i in instructions)
{
    PerformInstruction(i, supplyStacks);
}
PrintStacks(supplyStacks);

/// <summary>
/// Given an input row, determines if it is an instruction.
/// </summary>
bool IsInstructionRow(string row)
{
    return row.Contains("move");
}

/// <summary>
/// Given an array of input rows, parse all instructions into a list.
/// </summary>
List<Instruction> ParseInstructions(string[] rows)
{
    List<Instruction> instructions = new ();
    foreach (string row in rows)
    {
        if (IsInstructionRow(row))
        {
            instructions.Add(ParseInstruction(row));
        }
    }
    return instructions;
}

/// <summary>
/// Given an instruction input row, parse and return an Instruction
/// </summary>
Instruction ParseInstruction(string row)
{
    string[] elements = row.Split();
    int count = int.Parse(elements[1]);
    int from = int.Parse(elements[3]);
    int to = int.Parse(elements[5]);
    return new Instruction(count, from, to);
}

/// <summary>
/// Performs the specified instruction on the supply stacks
/// </summary>
void PerformInstruction(Instruction instruction, List<List<char>> stacks)
{
    for (int i = 0; i < instruction.Count; i++)
    {
        Move(instruction.From, instruction.To, stacks);
    }
}

/// <summary>
/// Moves the top most item from one stack to another
/// </summary>
void Move(int from, int to, List<List<char>> stacks)
{
    char container = stacks[from - 1][0];
    stacks[from - 1].RemoveAt(0);
    stacks[to - 1].Insert(0, container);
}

/// <summary>
/// Helper method which prints the current state of the stacks
/// </summary>
void PrintStacks(List<List<char>> stacks)
{
    for (int i = 0; i < stacks.Count; i++)
    {
        Console.WriteLine($"Stack {i + 1}: {string.Join(", ", stacks[i])}");
    }
}

/// <summary>
/// Given the input rows, Parses the Supply Stacks
/// </summary>
List<List<char>> ParseSupplyStacks(string[] rows)
{
    List<List<char>> stacks = InitStacks(GetNumberOfContainers(rows[0]));
    foreach (string row in rows)
    {
        if (IsContainerRow(row))
        {
            ParseContainerRow(row, stacks);
        }
    }
    return stacks;
}

/// <summary>
/// Given an input row, determines if it is a container row.
/// </summary>
bool IsContainerRow(string row)
{
    return row.Contains('[');
}

/// <summary>
/// Given a string representing a row of supply stacks,
/// adds each item found to the appropriate stack.
/// </summary>
void ParseContainerRow(string row, List<List<char>> stacks)
{
    for (int i = 0; i < stacks.Count; i++)
    {
        int colIx = i * 4 + 1;
        char ch = row[colIx];
        if (ch != ' ')
        {
            stacks[i].Add(ch);
        }
    }
}

/// <summary>
/// Given an input row, determines how many containers are needed
/// </summary>
int GetNumberOfContainers(string row)
{
    return (row.Length + 1)/4;
}

/// <summary>
/// Initializes a List containing the specified number of "supply stacks".
/// </summary>
List<List<char>> InitStacks(int count)
{
    List<List<char>> stacks = new ();
    for (int i = 0; i < count; i++)
    {
        stacks.Add(new List<char>());
    }
    return stacks;
}

record Instruction(int Count, int From, int To);