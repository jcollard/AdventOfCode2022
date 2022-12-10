public record Instruction(int Cycles, int Value)
{
    public static readonly Instruction NOOP = new(1, 0);

    public static Instruction ParseInstruction(string input)
    {
        string[] tokens = input.Split();
        return tokens[0] switch
        {
            "noop" => NOOP,
            "addx" => new Instruction(2, int.Parse(tokens[1])),
            _ => throw new Exception($"Could not parse instructions {input}"),
        };
    }

    public static List<Instruction> ParseInstructions(string[] input)
    {
        List<Instruction> instructions = new ();
        foreach (string row in input)
        {
            instructions.Add(ParseInstruction(row));
        }
        return instructions;
    }

}