public record Cave(HashSet<Position> Occupied, int MaxRow)
{
    public static readonly Position Origin = new (0, 500);
    private HashSet<Position> _sand = new ();
    private Stack<Position> _backTracker = new ();
    public int SandCount => _sand.Count;

    public bool DropSand()
    {
        if (_backTracker.Count == 0)
        {
            _backTracker.Push(Origin);
        }
        foreach(Position next in _backTracker.Peek().Next)
        {
            if (Occupied.Contains(next)) continue; // Don't check this space, 
            if (next.Row >= MaxRow) return false; // This space falls forever
            _backTracker.Push(next);
            return DropSand();
        }
        Position toPlace = _backTracker.Pop();
        if (toPlace.Row >= MaxRow)
        {
            return false;
        }
        _sand.Add(toPlace);
        Occupied.Add(toPlace);
        return true;
    }

    public static Cave Parse(string[] rows)
    {
        HashSet<Position> occupied = new();
        int maxRow = int.MinValue;
        foreach (string row in rows)
        {
            Position[] positions = row.Split(" -> ").Select(Position.Parse).ToArray();
            for (int i = 0; i < positions.Length - 1; i++)
            {
                maxRow = Math.Max(maxRow, positions[i].Row);
                maxRow = Math.Max(maxRow, positions[i+1].Row);
                occupied.UnionWith(ParseSegment(positions[i], positions[i+1]));
            }
        }
        return new Cave(occupied, maxRow);
    }

    public static HashSet<Position> ParseSegment(Position start, Position end)
    {
        HashSet<Position> ps = new();
        ps.Add(start);
        while (start != end)
        {
            int diffRow = Math.Sign(end.Row - start.Row);
            int diffCol = Math.Sign(end.Col - start.Col);
            start = new Position(start.Row + diffRow, start.Col + diffCol);
            ps.Add(start);
        }
        return ps;
    }

    public void PrintWindow(Position TopLeft, Position BottomRight)
    {
        for (int row = TopLeft.Row; row <= BottomRight.Row; row++)
        {
            for (int col = TopLeft.Col; col <= BottomRight.Col; col++)
            {
                Position p = new Position(row, col);
                if (_sand.Contains(p))
                {
                    Console.Write("o");
                }
                else if (Occupied.Contains(p))
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine();
        }
    }
}