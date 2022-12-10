string[] input = File.ReadAllLines("example.txt");
List<Instruction> instructions = Instruction.ParseInstructions(input);
CPU CRT = new (instructions);
int score = 0;
while (CRT.Cycle <= 240)
{
    CRT.Tick();
    if (CRT.Cycle == 20 || (CRT.Cycle + 20) % 40 == 0)
    {
        score += CRT.X * CRT.Cycle;
        Console.WriteLine($"Counter {CRT.Cycle} | Score: {score}");
    }
}
