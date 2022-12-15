namespace Tests;

public class CaveTest
{
    [Fact(Timeout = 5000)]
    public void TestParseCave()
    {
        string[] rows = new string[]
        {
            "498,4 -> 498,6 -> 496,6",
            "503,4 -> 502,4 -> 502,9 -> 494,9"
        };

        Cave cave = Cave.Parse(rows);
        string result = cave.PrintWindow(new Position(494, 0), new Position(503, 9));
        string[] expected = new string[]
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
        Assert.Equal(string.Join("\n", expected), result); 

        
    }

    [Fact(Timeout = 5000)]
    public void TestDropSand()
    {
        string[] rows = new string[]
        {
            "498,4 -> 498,6 -> 496,6",
            "503,4 -> 502,4 -> 502,9 -> 494,9"
        };

        Cave cave = Cave.Parse(rows);

        Assert.True(cave.DropSand());
        string result = cave.PrintWindow(new Position(494, 0), new Position(503, 9));
        string[] expected = new string[]
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
        Assert.Equal(string.Join("\n", expected), result); 

        Assert.True(cave.DropSand());
        result = cave.PrintWindow(new Position(494, 0), new Position(503, 9));
        expected = new string[]
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
        Assert.Equal(string.Join("\n", expected), result);

        Assert.True(cave.DropSand());
        result = cave.PrintWindow(new Position(494, 0), new Position(503, 9));
        expected = new string[]
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
        Assert.Equal(string.Join("\n", expected), result);

        Assert.True(cave.DropSand());
        result = cave.PrintWindow(new Position(494, 0), new Position(503, 9));
        expected = new string[]
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
        Assert.Equal(string.Join("\n", expected), result);

        Assert.True(cave.DropSand());
        result = cave.PrintWindow(new Position(494, 0), new Position(503, 9));
        expected = new string[]
        {
            "......+...",
            "..........",
            "..........",
            "..........",
            "....#...##",
            "....#...#.",
            "..###...#.",
            "......o.#.",
            "....oooo#.",
            "#########.",
        };
        Assert.Equal(string.Join("\n", expected), result);

        Assert.True(cave.DropSand());
        result = cave.PrintWindow(new Position(494, 0), new Position(503, 9));
        expected = new string[]
        {
            "......+...",
            "..........",
            "..........",
            "..........",
            "....#...##",
            "....#...#.",
            "..###...#.",
            ".....oo.#.",
            "....oooo#.",
            "#########.",
        };
        Assert.Equal(string.Join("\n", expected), result);

        Assert.True(cave.DropSand());
        result = cave.PrintWindow(new Position(494, 0), new Position(503, 9));
        expected = new string[]
        {
            "......+...",
            "..........",
            "..........",
            "..........",
            "....#...##",
            "....#...#.",
            "..###...#.",
            ".....ooo#.",
            "....oooo#.",
            "#########.",
        };
        Assert.Equal(string.Join("\n", expected), result);

        // 8 Drops so far

        for (int i = 8; i <= 24; i++)
        {
            Assert.True(cave.DropSand());
        }

        Assert.False(cave.DropSand());

        result = cave.PrintWindow(new Position(494, 0), new Position(503, 9));
        expected = new string[]
        {
            "......+...",
            "..........",
            "......o...",
            ".....ooo..",
            "....#ooo##",
            "...o#ooo#.",
            "..###ooo#.",
            "....oooo#.",
            ".o.ooooo#.",
            "#########.",
        };
        Assert.Equal(string.Join("\n", expected), result);

    }

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