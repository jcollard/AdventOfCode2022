public record Position(int X, int Y)
{
    public Position Down { get; } = new Position(X, Y + 1);
    public Position DownLeft { get; } = new Position(X - 1, Y + 1);
    public Position DownRight { get; } = new Position(X + 1, Y + 1);
    public static Position Parse(string pos)
    {
        string[] tokens = pos.Split(",");
        return new Position(int.Parse(tokens[0]), int.Parse(tokens[1]));
    }

    public static HashSet<Position> BuildSegment(Position start, Position end)
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
}