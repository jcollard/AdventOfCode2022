public record Piece(HashSet<Position> Rocks)
{
    public static Piece H = new (
        new HashSet<Position>()
        {
            new Position(0,0),
            new Position(0,1),
            new Position(0,2),
            new Position(0,3)
        }
    );

    public static Piece X = new (
        new HashSet<Position>()
        {
            new Position(0,1),
            new Position(1,0),
            new Position(1,1),
            new Position(1,2),
            new Position(2,1)
        }
    );

    public static Piece J = new (
        new HashSet<Position>()
        {
            new Position(2,2),
            new Position(1,2),
            new Position(0,0),
            new Position(0,1),
            new Position(0,2)
        }
    );

    public static Piece I = new (
        new HashSet<Position>()
        {
            new Position(0,0),
            new Position(1,0),
            new Position(2,0),
            new Position(3,0)
        }
    );

    public static Piece O = new (
        new HashSet<Position>()
        {
            new Position(0,0),
            new Position(0,1),
            new Position(1,0),
            new Position(1,1)
        }
    );

    public static IEnumerable<Piece> Pieces()
    {
        Piece[] pieces = new []{H, X, J, I, O};
        while (true)
        {
            foreach (Piece p in pieces)
            {
                yield return p;
            }
        }
    }

    public Piece Shift(Position Offset)
    {
        HashSet<Position> newPos = this.Rocks.Select(p => p.Shift(Offset)).ToHashSet();
        return new Piece(newPos);
    }

    public int Highest => Rocks.Max(p => p.Row);
    public int Lowest => Rocks.Min(p => p.Row);
    public int LeftMost => Rocks.Min(p => p.Col);
    public int RightMost => Rocks.Max(p => p.Col);

}

public record Position(int Row, int Col)
{
    public Position Shift(Position Offset) => new (Row + Offset.Row, Col + Offset.Col);
}