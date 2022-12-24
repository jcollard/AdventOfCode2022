string[] rows = File.ReadAllLines("input.txt");
Part2();
void Part1()
{
    Board b = Board.Parse(rows);
    Position start = new Position(0, 1);
    Position end = new Position(b.Height - 1, b.Width - 2);
    // while (true)
    // {
    //     // Console.Clear();
    //     Console.WriteLine(b);
    //     Console.ReadLine();
    //     b = b.Step();
    // }
    (int steps, _, _) = Solver.Solve(b, start, end);
    Console.WriteLine(steps);
}

void Part2()
{
    Board b = Board.Parse(rows);
    Position start = new Position(0, 1);
    Position end = new Position(b.Height - 1, b.Width - 2);
    // Position exit = new (board.Height - 1, board.Width - 2);
    Console.WriteLine("First time: ");
    (int first, Board newBoard, Position endP) = Solver.Solve(b, start, end);
    Console.WriteLine(first);
    Console.WriteLine("Second time: ");
    (int second, newBoard, endP) = Solver.Solve(newBoard, end, start);
    Console.WriteLine(second);
    Console.WriteLine("Third time: ");
    (int third, newBoard, endP) = Solver.Solve(newBoard, start, end);
    Console.WriteLine(third);

    Console.WriteLine($"First {first}, Second {second}, Third {third} = {first + second + third}");    
    
}