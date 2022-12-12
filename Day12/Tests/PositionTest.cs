namespace Tests;

public class PositionTest
{
    [Fact(Timeout = 5000)]
    public void TestNeighbors()
    {
        Position p = new Position(1, 1, null);
        HashSet<Position> expected = new () {
            new Position(0, 1, p),
            new Position(1, 0, p),
            new Position(1, 2, p),
            new Position(2, 1, p)
        };
        Assert.Equal(expected, p.Neighbors);
    }
}