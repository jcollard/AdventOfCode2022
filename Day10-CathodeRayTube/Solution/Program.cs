string[] rows = File.ReadAllLines("input.txt");
int regX = 1;
int cycle = 1;
int score = 0;
Operation Noop = new Operation(ProcessNoop, 1);
Console.Clear();
foreach (string input in rows)
{
    Process(input);
}
Console.WriteLine();
Console.WriteLine(score);

void Process(string input)
{
    string[] tokens = input.Split();
    Operation o = tokens[0] switch {
        "noop" => Noop,
        "addx" => new Operation(() => ProcessAddX(int.Parse(tokens[1])), 2),
        _ => throw new Exception("Something terrible has happened!")
    };
    if (!Tick(o.cycles))
    {
        return;
    }
    o.endAction.Invoke();
}

bool Tick(int ticks)
{
    if (ticks == 0) return true;
    if (cycle > 240) return false;
    if (cycle == 20 || (cycle + 20) % 40 == 0)
    {
        // Console.WriteLine($"Cycle: {cycle} | Reg X: {regX}");
        score += cycle * regX;
    }
    DrawPixel();
    Thread.Sleep(10);
    cycle++;
    return Tick(ticks - 1);
}

void DrawPixel()
{
    int row = (cycle - 1) / 40;
    int col = (cycle - 1) % 40;
    Console.SetCursorPosition(col, row);
    if (IsInWindow(col))
    {
        Console.Write("#");
    }
    else
    {
        Console.Write(" ");
    }
}

bool IsInWindow(int col)
{
    return col >= (regX-1) && col <= (regX+1);
}

void ProcessNoop()
{
}

void ProcessAddX(int x)
{
    regX += x;
}

record Operation(Action endAction, int cycles);