namespace Tests;

public class PositionTest
{

    [Fact]
    public void TestNeighbors()
    {
        Position p = new Position(0, 0);
        List<Position> result = p.Neighbors();
        List<Position> expected = new () 
        { 
            new Position(-1, 0), 
            new Position(0, 1), 
            new Position(1, 0), 
            new Position(0, -1) 
        };
        Assert.Equal(expected, result);

        p = new Position(3, 2);
        result = p.Neighbors();
        expected = new () 
        { 
            new Position(2, 2), 
            new Position(3, 3), 
            new Position(4, 2), 
            new Position(3, 1) 
        };
        Assert.Equal(expected, result);
    }

}