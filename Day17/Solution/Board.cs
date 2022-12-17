public record Board(HashSet<Position> Rocks, Piece Falling, int HighestPoint)
{
    public TopRow TopRow 
    {
        get 
        {
            int mask = 0;
            for (int col = 0; col < 7; col++)
            {
                if (Rocks.Contains(new Position(HighestPoint, col)))
                {
                    mask |= 1 << col;
                }
            }
            return new TopRow(mask);
        }
    } 
    public bool TryShift(Position offset, out Board update)
    {
        update = this;
        Piece tryPiece = Falling.Shift(offset);
        // Console.WriteLine($"TryPiece.RightMost: {tryPiece.RightMost}");
        if (tryPiece.Lowest < 0 || tryPiece.LeftMost < 0 || tryPiece.RightMost >= 7)
        {
            return false;
        }
        foreach (Position p in tryPiece.Rocks)
        {
            if (Rocks.Contains(p))
            {
                return false;
            }
        }
        update = this with { Falling = tryPiece };
        return true;
    }

    public Board SetPiece(Piece next)
    {
        HashSet<Position> nextRocks = Rocks.Union(Falling.Rocks).ToHashSet();
        int nextHighest = Math.Max(HighestPoint, Falling.Highest);
        int rowShift = nextHighest + 4;
        int colShift = next.LeftMost + 2;
        next = next.Shift(new Position(rowShift, colShift));
        return new Board(nextRocks, next, nextHighest);
    }

    public Board SpawnPiece(Piece next)
    {
        int rowShift = HighestPoint + 4;
        int colShift = next.LeftMost + 2;
        next = next.Shift(new Position(rowShift, colShift));
        return this with { Falling = next };
    }

    public void PrintBoard()
    {
        int top = this.HighestPoint + 7;
        for (int row = top; row >= 0; row--)
        {
            for (int col = 0; col < 7; col++)
            {
                Position p = new(row, col);
                if (Rocks.Contains(p))
                {
                    Console.Write("#");
                }
                else if (Falling.Rocks.Contains(p))
                {
                    Console.Write("@");
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