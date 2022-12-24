public record Solver
{

    public static (int, Board, Position) Solve(Board board, Position start, Position exit)
    {
        Board.MemoizedBoards = new ();
        int Depth = 0;
        int MaxCycle = (board.Width - 2) * (board.Height - 2);
        Console.WriteLine(MaxCycle);
        // Console.ReadLine();
        // Position start = new (0, 1);
        Queue<(Position, Board)> nextPosition = new ();
        HashSet<State> visited = new ()
        {
            new State(start, 0)
        };
        nextPosition.Enqueue((start, board));
        while (nextPosition.Count > 0)
        {
            (Position p, Board b) = nextPosition.Dequeue();
            if (b.TimeSteps > Depth)
            {
                Console.WriteLine($"At Depth {Depth}");
                Console.WriteLine($"Visited: {visited.Count}");
                Depth = b.TimeSteps;
                
            }
            // Console.WriteLine(b.ToString(p));
            // Console.WriteLine($"At position: {p}");
            if (p == exit)
            {
                return (b.TimeSteps, b with {TimeSteps = 0}, p); // Return time steps
            }
            Board nb = b.Step();
            List<Position> ns = p.Neighbors.ToList();
            ns.Add(p);
            ns = ns
                .Where(nb.InBounds)
                .Where(nb.IsOpen)
                .ToList();
            foreach (Position n in ns)
            {
                State s = new (n, nb.TimeSteps % MaxCycle);
                if (visited.Contains(s)) continue;
                // Console.WriteLine($"Possible Choice: {n}");
                visited.Add(s);
                nextPosition.Enqueue((n, nb));
            }
            // Console.ReadLine();
        }
        throw new Exception("No solution!");
    }

}

public record State(Position Position, int Cycle);