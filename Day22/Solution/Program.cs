string input = File.ReadAllText("input.txt");
Part2();
// TestEdge();

void TestEdge()
{
    string[] tokens = input.Split("\n\n");
    string[] map = tokens[0].Split("\n");
    string moves = tokens[1];
    Cube cube = Cube.Parse(map);

    // Face Position: Position (Row: 49, Col: 0)
    // Global Position: Position (Row: 199, Col: 0)

    Position p = new Position(49, 0);
    Facing f = Facing.West;
    Face face = cube.F2;
    (Face newFace, Position position, Facing facing) = cube.F2.NextPosition(p, f);
    Console.WriteLine(newFace == cube.F3);
    Console.WriteLine($"{face.ID} => {newFace.ID}");
    Console.WriteLine($"{f} => {facing}");
    Console.WriteLine($"{p} => {position}");
}


void TestRotatePosition()
{
    Position p = new Position(1, 0);
    Rotation r = new (4);
    Console.WriteLine($"{p} -- {r}");
    Console.WriteLine(r.RotateCW(p, 4));

}

void TestRotate()
{
    char[][] data = new []{
        "a01b".ToCharArray(),
        "7892".ToCharArray(),
        "6103".ToCharArray(),
        "d54c".ToCharArray(),
    };
    Console.WriteLine(string.Join("\n",data.Select(s => string.Join("", s))));
    Console.WriteLine();
    Rotation r = new (0);
    r.RotateCW(data);

    
    r = new (1);
    r.RotateCounterCW(data);
    Console.WriteLine(string.Join("\n",data.Select(s => string.Join("", s))));
    Console.WriteLine();
}

void Part2()
{
    string[] tokens = input.Split("\n\n");
    string[] map = tokens[0].Split("\n");
    string moves = tokens[1];
    Cube cube = Cube.Parse(map);

    Queue<Move> ms = Move.Parse(moves);
    CubeSolver s = new (Cube.Parse(map), ms);
    do
    {
        // Console.Clear();
        // Console.WriteLine(s);
        // Console.WriteLine($"Face Position: {s.Position}");
        // Console.WriteLine($"Global Position: {s.GlobalPosition()}");
        // Console.WriteLine(s.Moves.Count > 0 ? s.Moves.Peek() : "Done");
        // Console.ReadLine();
    }
    while (s.Step());
    Position p = s.GlobalPosition();
    int sum = (p.Row + 1) * 1000 + (p.Col + 1) * 4 + s.Facing.ToInt();
    Console.WriteLine($"Result: {s.Position} and {s.Facing} = {sum}");
}

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