public record Position(int Row, int Col)
{
    public Position[] Next => new Position[]
    {
        this with { Row = Row + 1},
        this with { Row = Row + 1, Col = Col - 1},
        this with { Row = Row + 1, Col = Col + 1},
    };

    public static Position Parse(string posXY)
    {
        string[] tokens = posXY.Split(",");
        return new Position(int.Parse(tokens[1]), int.Parse(tokens[0]));
    }
}