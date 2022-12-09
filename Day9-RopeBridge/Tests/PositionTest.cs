namespace Tests;

public class PositionTest
{
    [Fact]
    public void TestMove()
    {
        Position p = new Position(0, 0);
        Position result = Position.Move(p, Move.RIGHT);
        Position expected = new Position(1, 0);
        Assert.Equal(expected, result);

        result = Position.Move(result, Move.UP);
        expected = new Position(1, 1);
        Assert.Equal(expected, result);

        result = Position.Move(result, Move.LEFT);
        expected = new Position(0, 1);
        Assert.Equal(expected, result);

        result = Position.Move(result, Move.DOWN);
        expected = new Position(0, 0);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestNoFollow()
    {
        Position head = new Position(1, -1);
        Position tail = new Position(0, 0);
        Position result = Position.Follow(head, tail);
        Position expected = new Position(0, 0);
        Assert.Equal(expected, result);

        head = new Position(1, 0);
        result = Position.Follow(head, tail);
        Assert.Equal(expected, result);

        head = new Position(1, 1);
        result = Position.Follow(head, tail);
        Assert.Equal(expected, result);

        head = new Position(0, -1);
        result = Position.Follow(head, tail);
        Assert.Equal(expected, result);

        head = new Position(0, 0);
        result = Position.Follow(head, tail);
        Assert.Equal(expected, result);

        head = new Position(0, 1);
        result = Position.Follow(head, tail);
        Assert.Equal(expected, result);

        head = new Position(-1, -1);
        result = Position.Follow(head, tail);
        Assert.Equal(expected, result);

        head = new Position(0, 0);
        result = Position.Follow(head, tail);
        Assert.Equal(expected, result);

        head = new Position(1, 1);
        result = Position.Follow(head, tail);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestFollowEast()
    {
        Position head = new Position(2, 0);
        Position tail = new Position(0, 0);
        Position result = Position.Follow(head, tail);
        Position expected = new Position(1, 0);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestFollowWest()
    {
        Position head = new Position(-2, 0);
        Position tail = new Position(0, 0);
        Position result = Position.Follow(head, tail);
        Position expected = new Position(-1, 0);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestFollowNorth()
    {

        Position head = new Position(0, 2);
        Position tail = new Position(0, 0);
        Position result = Position.Follow(head, tail);
        Position expected = new Position(0, 1);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestFollowSouth()
    {
        Position head = new Position(0, -2);
        Position tail = new Position(0, 0);
        Position result = Position.Follow(head, tail);
        Position expected = new Position(0, -1);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestNorthEastFollow()
    {
        Position head = new Position(2, 1);
        Position tail = new Position(0, 0);
        Position result = Position.Follow(head, tail);
        Position expected = new Position(1, 1);
        Assert.Equal(expected, result);

        head = new Position(1, 2);
        tail = new Position(0, 0);
        result = Position.Follow(head, tail);
        expected = new Position(1, 1);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestSouthEastFollow()
    {
        Position head = new Position(2, -1);
        Position tail = new Position(0, 0);
        Position result = Position.Follow(head, tail);
        Position expected = new Position(1, -1);
        Assert.Equal(expected, result);

        head = new Position(1, -2);
        tail = new Position(0, 0);
        result = Position.Follow(head, tail);
        expected = new Position(1, -1);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestSouthWestFollow()
    {
        Position head = new Position(-2, -1);
        Position tail = new Position(0, 0);
        Position result = Position.Follow(head, tail);
        Position expected = new Position(-1, -1);
        Assert.Equal(expected, result);

        head = new Position(-1, -2);
        tail = new Position(0, 0);
        result = Position.Follow(head, tail);
        expected = new Position(-1, -1);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestNorthWestFollow()
    {
        Position head = new Position(-2, 1);
        Position tail = new Position(0, 0);
        Position result = Position.Follow(head, tail);
        Position expected = new Position(-1, 1);
        Assert.Equal(expected, result);

        head = new Position(-1, 2);
        tail = new Position(0, 0);
        result = Position.Follow(head, tail);
        expected = new Position(-1, 1);
        Assert.Equal(expected, result);
    }
}