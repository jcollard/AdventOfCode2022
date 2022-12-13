public record Position(int Row, int Col)
{
    public Position North => new Position(Row - 1, Col);
    public Position South => new Position(Row + 1, Col);
    public Position East => new Position(Row, Col + 1);
    public Position West => new Position(Row, Col - 1);
    public List<Position> Neighbors()
    {
        // For testability, the order here is important
        return new List<Position>() { North, East, South, West };
    }
}