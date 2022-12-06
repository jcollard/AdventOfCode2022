List<List<char>> testSupplyStacks = new ();
testSupplyStacks.Add(new List<char>());

// I can index a supplyStack using an index
List<char> testStack = testSupplyStacks[0];

// I can "push" to the top of my stack using .Insert(0, el)
testStack.Insert(0, 'A');

// I can access the "top" element of a stack using index 0
char top = testStack[0];

// I can remove the "top" element of a stack using .RemoveAt(0)
testStack.RemoveAt(0);

string row = "    [D]    ";
List<List<char>> stacks = InitSupplyStacks(row);
Console.WriteLine($"{row} (3) => {stacks.Count}");

row = "[P]     [L]         [T]            ";
stacks = InitSupplyStacks(row);
Console.WriteLine($"{row} (9) => {stacks.Count}");


Console.WriteLine("Test ParseRow:");
row = "    [D]    ";
stacks = InitSupplyStacks(row);
ParseContainerRow(row, stacks);
Console.WriteLine(row);
PrintStacks(stacks);
row = "[N] [C]    ";
Console.WriteLine(row);
ParseContainerRow(row, stacks);
PrintStacks(stacks);
row = "[Z] [M] [P]";
Console.WriteLine(row);
ParseContainerRow(row, stacks);
PrintStacks(stacks);

string[] rows = File.ReadAllLines("sample.txt");
List<List<char>> supplyStacks = ParseSupplyStacks(rows);
Console.WriteLine("\nTest ParseSupplyStacks");
PrintStacks(supplyStacks);

Instruction instruction;
Console.WriteLine("\nTest Parse Instruction");
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

Console.WriteLine("\n Test Parse Instructions:");
rows = File.ReadAllLines("sample.txt");
List<Instruction> instructions = ParseInstructions(rows);
Console.WriteLine("Instructions: " + String.Join(", ", instructions));

stacks = ParseSupplyStacks(rows);
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

Console.WriteLine("\nTest Perform Instruction");
PerformInstruction(new Instruction(1, 2, 1), stacks);
PrintStacks(stacks);
Console.WriteLine();
PerformInstruction(new Instruction(3, 1, 3), stacks);
PrintStacks(stacks);
Console.WriteLine();
PerformInstruction(new Instruction(2, 2, 1), stacks);
PrintStacks(stacks);
Console.WriteLine();
PerformInstruction(new Instruction(1, 1, 2), stacks);
PrintStacks(stacks);

Console.Clear();
Console.WriteLine("Final Stacks:");
rows = File.ReadAllLines("sample.txt");
supplyStacks = ParseSupplyStacks(rows);
instructions = ParseInstructions(rows);
foreach (Instruction i in instructions)
{
    PerformInstruction(i, supplyStacks);
}
PrintStacks(supplyStacks);

void PerformInstruction(Instruction instruction, List<List<char>> stacks)
{
    for (int i = 0; i < instruction.Count; i++)
    {
        Move(instruction.From, instruction.To, stacks);
    }
}

void Move(int from, int to, List<List<char>> stacks)
{
    char container = stacks[from - 1][0];
    stacks[from - 1].RemoveAt(0);
    stacks[to - 1].Insert(0, container);
}

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

bool IsInstructionRow(string row)
{
    return row.Contains("move");
}

Instruction ParseInstruction(string row)
{
    string[] elements = row.Split();
    int count = int.Parse(elements[1]);
    int from = int.Parse(elements[3]);
    int to = int.Parse(elements[5]);
    return new Instruction(count, from, to);
}

List<List<char>> ParseSupplyStacks(string[] rows)
{
    List<List<char>> stacks = InitSupplyStacks(rows[0]);
    foreach (string row in rows)
    {
        if (IsContainerRow(row))
        {
            ParseContainerRow(row, stacks);
        }
    }
    return stacks;
}

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
/// Helper method which prints the current state of the stacks
/// </summary>
void PrintStacks(List<List<char>> stacks)
{
    for (int i = 0; i < stacks.Count; i++)
    {
        Console.WriteLine($"Stack {i + 1}: {string.Join(", ", stacks[i])}");
    }
}

List<List<char>> InitSupplyStacks(string row)
{
    List<List<char>> stacks = new ();
    int count = (row.Length + 1)/4;
    for (int i = 0; i < count; i++)
    {
        stacks.Add(new List<char>());
    }
    return stacks;
}