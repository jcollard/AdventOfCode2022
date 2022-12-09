public record Position(int X, int Y)
{
    public static Position Move(Position p, Move move)
    {
        return new Position(p.X + move.X, p.Y + move.Y);
    }

    public static Position Follow(Position head, Position tail)
    {
        if (IsAdjacent(head, tail))
        {
            return tail;
        }

        int offX = FindSign(head.X - tail.X);
        int offY = FindSign(head.Y - tail.Y);
        return new Position(tail.X + offX, tail.Y + offY);
    }

    public static bool IsAdjacent(Position p0, Position p1)
    {
        int xDist = Math.Abs(p0.X - p1.X);
        int yDist = Math.Abs(p0.Y - p1.Y);
        return xDist <= 1 && yDist <= 1;
    }

    public static int FindSign(int n)
    {
        if (n == 0)
        {
            return 0;
        }
        else if (n > 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
}