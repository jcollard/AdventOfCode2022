public record Cave(HashSet<Position> Rocks)
{
    public static readonly Position Origin = new (500, 0);
    public HashSet<Position> SettledSand { get; } = new ();

    public (Position TopLeft, Position BottomRight) Bounds = Position.FindBounds(Rocks);

    public bool Step()
    {
        Sand s = new Sand(Origin, this);
        while (s.Fall())
        {
            if (IsAtBottom(s.Position))
            {
                return false;
            }
        }
        SettledSand.Add(s.Position);
        return true;
    }

    public bool IsOccupied(Position p)
    {
        return Rocks.Contains(p) || SettledSand.Contains(p);
    }

    public bool IsAtBottom(Position p)
    {
        return p.Y >= Bounds.BottomRight.Y;
    }

        public static Cave Parse(string[] rows)
    {
        HashSet<Position> occupied = new();
        foreach (string row in rows)
        {
            Position[] positions = row.Split(" -> ").Select(Position.Parse).ToArray();
            for (int i = 0; i < positions.Length - 1; i++)
            {
                occupied.UnionWith(ParseSegment(positions[i], positions[i+1]));
            }
        }
        return new Cave(occupied);
    }

    public static HashSet<Position> ParseSegment(Position start, Position end)
    {
        HashSet<Position> ps = new();
        ps.Add(start);
        while (start != end)
        {
            int diffX = Math.Sign(end.X - start.X);
            int diffY = Math.Sign(end.Y - start.Y);
            start = new Position(start.X + diffX, start.Y + diffY);
            ps.Add(start);
        }
        return ps;
    }

    public void Print()
    {
        (Position TopLeft, Position BottomRight) = Position.FindBounds(Rocks, 1);

        for (int row = TopLeft.Y; row <= BottomRight.Y; row++)
        {
            for (int col = TopLeft.X; col <= BottomRight.X; col++)
            {
                Position p = new Position(col, row);
                if (SettledSand.Contains(p))
                {
                    Console.Write("o");
                }
                else if (this.IsOccupied(p))
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