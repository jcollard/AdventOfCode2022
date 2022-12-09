namespace Tests;

public class MoveTest
{
    [Fact]
    public void TestParse()
    {
        string[] example = new string[]{
            "R 4",
            "L 2",
            "U 3",
            "D 2"
        };
        List<Move> result = Move.Parse(example);
        List<Move> expected = new ()
        {
            Move.RIGHT, Move.RIGHT, Move.RIGHT, Move.RIGHT,
            Move.LEFT, Move.LEFT,
            Move.UP, Move.UP, Move.UP,
            Move.DOWN, Move.DOWN
        };;
        Assert.Equal(expected, result);
    }
}