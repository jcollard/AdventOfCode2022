public record Position(int X, int Y)
{
    public Position Down => new Position(X, Y + 1);
    public Position DownLeft => new Position(X - 1, Y + 1);
    public Position DownRight => new Position(X + 1, Y + 1);
    public static Position Parse(string pos)
    {
        string[] tokens = pos.Split(",");
        return new Position(int.Parse(tokens[0]), int.Parse(tokens[1]));
    }

    public static (Position TopLeft, Position BottomRight) FindBounds(HashSet<Position> positions, int padding = 0)
    {
        int top = 0;
        int bottom = 0;
        int left = int.MaxValue;
        int right = int.MinValue;
        Position br = new Position(int.MinValue, int.MinValue);
        foreach (Position position in positions)
        {
            top = Math.Min(position.Y, top);
            bottom = Math.Max(position.Y, bottom);
            left = Math.Min(position.X, left);
            right = Math.Max(position.X, right);
        }
        return (new Position(left - padding, top - padding), new Position(right + padding, bottom + padding));
    }
}