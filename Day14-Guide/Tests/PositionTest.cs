namespace Tests;

public class PositionTest
{
    [Fact]
    public void TestParsePosition()
    {
        Position result = Position.Parse("12,7");
        Position expected = new Position(12, 7);
        Assert.Equal(expected, result);

        result = Position.Parse("0,0");
        expected = new Position(0, 0);
        Assert.Equal(expected, result);

        result = Position.Parse("490,63");
        expected = new Position(490, 63);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestBuildSegment()
    {
        Position start = new Position(498, 4);
        Position end = new Position(498, 6);
        HashSet<Position> result = Position.BuildSegment(start, end);
        HashSet<Position> expected = new ()
        {
            start, new Position(498, 5), end
        };
        Assert.Equal(expected, result);

        start = new Position(498, 6);
        end = new Position(496, 6);
        result = Position.BuildSegment(start, end);
        expected = new ()
        {
            start, new Position(497, 6), end
        };
        Assert.Equal(expected, result);

        start = new Position(503, 4);
        end = new Position(502, 4);
        result = Position.BuildSegment(start, end);
        expected = new ()
        {
            start, end
        };
        Assert.Equal(expected, result);

        start = new Position(502, 4);
        end = new Position(502, 9);
        result = Position.BuildSegment(start, end);
        expected = new ()
        {
            start, 
            new Position(502, 5),
            new Position(502, 6),
            new Position(502, 7),
            new Position(502, 8),
            end
        };
        Assert.Equal(expected, result);

        start = new Position(502, 9);
        end = new Position(494, 9);
        result = Position.BuildSegment(start, end);
        expected = new ()
        {
            start, 
            new Position(501, 9),
            new Position(500, 9),
            new Position(499, 9),
            new Position(498, 9),
            new Position(497, 9),
            new Position(496, 9),
            new Position(495, 9),
            end
        };
        Assert.Equal(expected, result);
    }
}