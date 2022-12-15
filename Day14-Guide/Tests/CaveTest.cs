namespace Tests;

public class CaveTest
{
    [Fact(Timeout = 5000)]
    public void TestPrintWindow()
    {
        string[] rows = new string[]
        {
            "......+...",
            "..........",
            "..........",
            "..........",
            "....#...##",
            "....#...#.",
            "..###...#.",
            "........#.",
            "........#.",
            "#########.",
        };

        HashSet<Position> rocks = new ();
        rocks.UnionWith(Position.BuildSegment(new Position(498,4), new Position(498, 6)));
        rocks.UnionWith(Position.BuildSegment(new Position(498,6), new Position(496, 6)));
        rocks.UnionWith(Position.BuildSegment(new Position(503,4), new Position(502, 4)));
        rocks.UnionWith(Position.BuildSegment(new Position(502,4), new Position(502, 9)));
        rocks.UnionWith(Position.BuildSegment(new Position(502,9), new Position(494, 9)));
        Cave cave = new Cave(rocks, 9);
        string result = cave.PrintWindow(new Position(494, 0), new Position(503, 9));
        string expected = string.Join("\n", rows);
        Assert.Equal(expected, result); 

        rows = new string[]
        {
            "......+...",
            "..........",
            "..........",
            "..........",
            "....#...##",
            "....#...#.",
            "..###...#.",
            "........#.",
            "......o.#.",
            "#########.",
        };

        cave.SettledSand.Add(new Position(500, 8));
        result = cave.PrintWindow(new Position(494, 0), new Position(503, 9));
        expected = string.Join("\n", rows);
        Assert.Equal(expected, result);

        rows = new string[]
        {
            "......+...",
            "..........",
            "..........",
            "..........",
            "....#...##",
            "....#...#.",
            "..###...#.",
            "........#.",
            ".....oo.#.",
            "#########.",
        };

        cave.SettledSand.Add(new Position(499, 8));
        result = cave.PrintWindow(new Position(494, 0), new Position(503, 9));
        expected = string.Join("\n", rows);
        Assert.Equal(expected, result);

        rows = new string[]
        {
            "......+...",
            "..........",
            "..........",
            "..........",
            "....#...##",
            "....#...#.",
            "..###...#.",
            "........#.",
            ".....ooo#.",
            "#########.",
        };

        cave.SettledSand.Add(new Position(501, 8));
        result = cave.PrintWindow(new Position(494, 0), new Position(503, 9));
        expected = string.Join("\n", rows);
        Assert.Equal(expected, result);

        rows = new string[]
        {
            "......+...",
            "..........",
            "..........",
            "..........",
            "....#...##",
            "....#...#.",
            "..###...#.",
            "......o.#.",
            ".....ooo#.",
            "#########.",
        };

        cave.SettledSand.Add(new Position(500, 7));
        result = cave.PrintWindow(new Position(494, 0), new Position(503, 9));
        expected = string.Join("\n", rows);
        Assert.Equal(expected, result);

    }
}