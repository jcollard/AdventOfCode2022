public record Blizzard(Position Position, Func<Position, Position> NextPosition, char Symbol)
{
    public Blizzard Next() => this with { Position = NextPosition.Invoke(Position) };

    public override string ToString()
    {
        return $"Blizzard @ ({Position.Row}, {Position.Col}) '{Symbol}'";
    }
}