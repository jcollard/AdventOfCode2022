public record Position(int Row, int Col)
{
    public Position East => this with { Col = Col + 1 };
    public Position West => this with { Col = Col - 1 };
    public Position North => this with { Row = Row - 1 };
    public Position South => this with { Row = Row + 1 };

    public Position FromFacing(Facing f)
    {
        return f switch {
            Facing.East => East,
            Facing.West => West,
            Facing.South => South,
            Facing.North => North,
            _ => throw new Exception($"Invalid facing {f}")
        };
    }

    public override string ToString()
    {
        return $"Position (Row: {Row}, Col: {Col})";
    }
}
