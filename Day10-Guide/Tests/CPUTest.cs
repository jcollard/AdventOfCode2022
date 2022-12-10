namespace Tests;

public class CPUTest
{
    
    [Fact]
    public void TestLoadInstruction()
    {
        List<Instruction> example = new (){
            Instruction.NOOP,
            new Instruction(2, 3),
            Instruction.NOOP,
            new Instruction(2, -5)
        };
        
        CPU cpu = new CPU(example);
        Assert.Equal(Instruction.NOOP, cpu.CurrentInstruction);
        Assert.Equal(1, cpu.CyclesRemaining);

        cpu.LoadNextInstruction();
        Assert.Equal(new Instruction(2, 3), cpu.CurrentInstruction);
        Assert.Equal(2, cpu.CyclesRemaining);

        cpu.LoadNextInstruction();
        Assert.Equal(Instruction.NOOP, cpu.CurrentInstruction);
        Assert.Equal(1, cpu.CyclesRemaining);
        
        cpu.LoadNextInstruction();
        Assert.Equal(new Instruction(2, -5), cpu.CurrentInstruction);
        Assert.Equal(2, cpu.CyclesRemaining);
    }

    [Fact]
    public void TestTick()
    {
        List<Instruction> example = new (){
            Instruction.NOOP,
            new Instruction(2, 3),
            new Instruction(2, -5)
        };
        CPU cpu = new CPU(example);

        // At the start of the first cycle
        Assert.Equal(1, cpu.Cycle);
        Assert.Equal(1, cpu.X);
        Assert.Equal(1, cpu.CyclesRemaining);
        Assert.Equal(Instruction.NOOP, cpu.CurrentInstruction);

        cpu.Tick();

        // At the end of the first cycle / start of second cycle
        Assert.Equal(2, cpu.Cycle);
        Assert.Equal(1, cpu.X);
        Assert.Equal(2, cpu.CyclesRemaining);
        Assert.Equal(new Instruction(2, 3), cpu.CurrentInstruction);
        cpu.Tick();

        // At the end of the second cycle / start of third cycle
        Assert.Equal(3, cpu.Cycle);
        Assert.Equal(1, cpu.X);
        Assert.Equal(1, cpu.CyclesRemaining);
        Assert.Equal(new Instruction(2, 3), cpu.CurrentInstruction);
        cpu.Tick();

        // At the end of the third cycle / start of fourth cycle
        Assert.Equal(4, cpu.Cycle);
        Assert.Equal(4, cpu.X);
        Assert.Equal(2, cpu.CyclesRemaining);
        Assert.Equal(new Instruction(2, -5), cpu.CurrentInstruction);
        cpu.Tick();

        // At the end of the fourth cycle / start of fifth cycle
        Assert.Equal(5, cpu.Cycle);
        Assert.Equal(4, cpu.X);
        Assert.Equal(1, cpu.CyclesRemaining);
        Assert.Equal(new Instruction(2, -5), cpu.CurrentInstruction);
        cpu.Tick();

        // At the end of the fifth cycle / start of sixth cycle
        Assert.Equal(6, cpu.Cycle);
        Assert.Equal(-1, cpu.X);
        Assert.Equal(0, cpu.CyclesRemaining);
        Assert.Equal(new Instruction(2, -5), cpu.CurrentInstruction);
    }
}