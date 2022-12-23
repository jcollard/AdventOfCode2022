// See https://aka.ms/new-console-template for more information
string[] rows = File.ReadAllLines("input.txt");

Part2();

void Part2()
{
    Grid grid = Grid.Parse(rows);
    int rounds = 1;
    for(; grid.Step(); rounds++);
    Console.WriteLine(rounds);
}

void Part1()
{
    Grid grid = Grid.Parse(rows);

    for (int i = 1; i <= 10; i++)
    {
        // Console.Clear();
        // Console.WriteLine(i);
        // Console.WriteLine(grid);
        // Console.WriteLine(grid.EmptyTiles);
        // Console.WriteLine(grid.Occupied.Count);
        // Console.ReadLine();
        grid.Step();
    }
    Console.WriteLine(grid.EmptyTiles);
}