string[] input = File.ReadAllLines("example.txt");
List<Instruction> instructions = Instruction.ParseInstructions(input);
CPU CRT = new (instructions);
int score = 0;
while (CRT.NextCycle <= 220)
{
    CRT.Tick();
    if (CRT.NextCycle == 20 || (CRT.NextCycle + 20) % 40 == 0)
    {
        score += CRT.X * CRT.NextCycle;
        Console.WriteLine($"Counter {CRT.NextCycle} | Score: {score}");
    }
}
