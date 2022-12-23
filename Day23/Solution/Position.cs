public record Position(int Row, int Col)
{
    public Position N => this with { Row = Row - 1 };
    public Position E => this with { Col = Col + 1 };
    public Position S => this with { Row = Row + 1 };
    public Position W => this with { Col = Col - 1 };
    public Position NE => this with { Row = N.Row, Col = E.Col };
    public Position NW => this with { Row = N.Row, Col = W.Col };
    public Position SW => this with { Row = S.Row, Col = W.Col };
    public Position SE => this with { Row = S.Row, Col = E.Col };
    public Position[] Neighbors => new []{ N, E, S, W, NE, NW, SW, SE};

    public override string ToString() => $"Position (Row: {Row}, Col: {Col})";
}