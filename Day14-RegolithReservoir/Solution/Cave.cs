public record Cave(HashSet<Position> Rocks, int BottomY)
{
    public static readonly Position Origin = new(500, 0);
    public HashSet<Position> SettledSand { get; } = new();

    public bool DropSand()
    {
        Sand s = new Sand(Origin, this);
        while (s.Fall())
        {
            if (IsFinished(s.Position))
            {
                return false;
            }
        }
        SettledSand.Add(s.Position);
        return true;
    }

    public bool IsFinished(Position p)
    {
        return p.Y >= BottomY;
    }

    public bool IsOccupied(Position p)
    {
        return Rocks.Contains(p) || SettledSand.Contains(p);
    }

    public static Cave Parse(string[] rows)
    {
        HashSet<Position> occupied = new();
        int bottomY = 0;
        foreach (string row in rows)
        {
            string[] positions = row.Split(" -> ");
            for (int i = 0; i < positions.Length - 1; i++)
            {
                Position start = Position.Parse(positions[i]);
                Position end = Position.Parse(positions[i + 1]);
                bottomY = Math.Max(bottomY, start.Y);
                bottomY = Math.Max(bottomY, end.Y);
                HashSet<Position> segment = Position.BuildSegment(start, end);
                occupied.UnionWith(segment);
            }
        }
        return new Cave(occupied, bottomY);
    }

    public string PrintWindow(Position TopLeft, Position BottomRight)
    {
        string cave = string.Empty;
        for (int y = TopLeft.Y; y <= BottomRight.Y; y++)
        {
            for (int x = TopLeft.X; x <= BottomRight.X; x++)
            {
                Position p = new Position(x, y);
                cave += PositionToSymbol(p);
            }
            cave += "\n";
        }
        return cave.Trim();
    }

    private char PositionToSymbol(Position p)
    {
        if (SettledSand.Contains(p))
        {
            return 'o';
        }
        else if (Rocks.Contains(p))
        {
            return '#';
        }
        else if (p == Origin)
        {
            return '+';
        }
        else
        {
            return '.';
        }
    }

}