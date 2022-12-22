string input = File.ReadAllText("input.txt");

void Part1()
{
    string[] tokens = input.Split("\n\n");
    string[] map = tokens[0].Split("\n");
    string moves = tokens[1];
    // Console.WriteLine(map.Length);
    // Console.WriteLine(map[0].Length);
    // Console.ReadLine();
    Queue<Move> ms = Move.Parse(moves);
    Solver s = new Solver(Board.Parse(map), ms);
    do
    {
        // Console.Clear();
        // Console.WriteLine(s);
        // Console.WriteLine(s.Moves.Count > 0 ? s.Moves.Peek() : "Done");
        // Console.ReadLine();
    }
    while (s.Step());
    int sum = (s.Position.Row + 1) * 1000 + (s.Position.Col + 1) * 4 + s.Facing.ToInt();
    Console.WriteLine($"Result: {s.Position} and {s.Facing} = {sum}");
}