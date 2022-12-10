public record CPU(List<Instruction> Instructions)
{
    public int X { get; private set; } = 1;
    public int Cycle { get; private set; } = 1;
    public int CyclesRemaining { get; private set; } = Instructions[0].Cycles;
    public Instruction CurrentInstruction { get; private set; } = Instructions[0];
    private int _instIx = 0;

    public void LoadNextInstruction()
    {
        if (this._instIx < this.Instructions.Count - 1)
        {
            this._instIx++;
            this.CurrentInstruction = this.Instructions[this._instIx];
            this.CyclesRemaining = this.CurrentInstruction.Cycles;
        }   
    }

    public void Tick()
    {
        this.Cycle++;
        this.CyclesRemaining--;
        if (this.CyclesRemaining <= 0)
        {
            this.X += this.CurrentInstruction.Value;
            this.LoadNextInstruction();
        }
    }
}