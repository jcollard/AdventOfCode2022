public record Position(int Row, int Col, Position? From)
{
    public Position North => this with { Row = Row - 1, From = this };
    public Position South => this with { Row = Row + 1, From = this };
    public Position East => this with { Col = Col + 1, From = this };
    public Position West => this with { Col = Col - 1, From = this };
    public HashSet<Position> Neighbors => new () { North, South, East, West };
    public (int Row, int Col) AsPair => (Row, Col);

    public override string ToString()
    {
        return $"Position {AsPair} From: {From?.AsPair}";
    }

    public List<Position> Path()
    {
        List<Position> backTrack = new () { this };
        Position current = this;
        while (current.From != null)
        {
            current = current.From;
            backTrack.Add(current);
        }
        backTrack.Reverse();
        return backTrack;
    }

}