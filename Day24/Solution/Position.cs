public record Position(int Row, int Col)
{
    public Position N => this with { Row = Row - 1 };
    public Position E => this with { Col = Col + 1 };
    public Position S => this with { Row = Row + 1 };
    public Position W => this with { Col = Col - 1 };
    public Position[] Neighbors => new []{N, E, S, W};

    public Position Wrap(int width, int height)
    {
        if (Row >= 1 && Row < height - 2 && Col >= 1 && Col < width - 2)
        {
            return this;
        }
        int newRow = Row == 0 ? height - 2 : Row;
        newRow = newRow == height - 1 ? 1 : newRow;
        int newCol = Col == 0 ? width - 2 : Col;
        newCol = newCol == width - 1 ? 1 : newCol;
        return new Position(newRow, newCol);
    }

    public override string ToString()
    {
        return $"Position (Row: {Row}, Col: {Col})";
    }
}