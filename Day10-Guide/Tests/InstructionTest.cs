namespace Tests;

public class InstructionTest
{
    [Fact]
    public void TestParseNOOP()
    {
        Instruction result = Instruction.ParseInstruction("noop");
        Instruction expected = Instruction.NOOP;
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestParseAddX()
    {
        Instruction result = Instruction.ParseInstruction("addx 5");
        Instruction expected = new Instruction(2, 5);
        Assert.Equal(expected, result);

        result = Instruction.ParseInstruction("addx -17");
        expected = new Instruction(2, -17);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestParseInstructions()
    {
        string[] sample = new string[]{
            "noop",
            "addx 15",
            "noop",
            "noop",
            "addx -11",
            "addx 4"
        };
        List<Instruction> result = Instruction.ParseInstructions(sample);
        List<Instruction> expected = new()
        {
            Instruction.NOOP,
            new Instruction(2, 15),
            Instruction.NOOP,
            Instruction.NOOP,
            new Instruction(2, -11),
            new Instruction(2, 4),
        };
        Assert.Equal(expected, result);
    }
}