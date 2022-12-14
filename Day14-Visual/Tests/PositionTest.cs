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
}