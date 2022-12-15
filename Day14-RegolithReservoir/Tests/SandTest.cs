namespace Tests;

public class SandTest
{
    [Fact(Timeout = 5000)]
    public void TestFall()
    {
        string[] rows = new string[]
        {
            "498,4 -> 498,6 -> 496,6",
            "503,4 -> 502,4 -> 502,9 -> 494,9"
        };

        Cave cave = Cave.Parse(rows);

        //    0 "......+...",
        //    1 "..........",
        //    2 "..........",
        //    3 "..........",
        //    4 "....#...##",
        //    5 "....#...#.",
        //    6 "..###...#.",
        //    7 "........#.",
        //    8 "........#.",
        //    9 "#########.",
        //       4     5
        //       9     0
        //       4567890123
        
        Sand s = new (new Position(500, 0), cave);
        Assert.True(s.Fall());
        Assert.Equal(new Position(500, 1), s.Position);
        Assert.True(s.Fall());
        Assert.Equal(new Position(500, 2), s.Position);
        Assert.True(s.Fall());
        Assert.Equal(new Position(500, 3), s.Position);
        Assert.True(s.Fall());
        Assert.Equal(new Position(500, 4), s.Position);
        Assert.True(s.Fall());
        Assert.Equal(new Position(500, 5), s.Position);
        Assert.True(s.Fall());
        Assert.Equal(new Position(500, 6), s.Position);
        Assert.True(s.Fall());
        Assert.Equal(new Position(500, 7), s.Position);
        Assert.True(s.Fall());
        Assert.Equal(new Position(500, 8), s.Position);
        Assert.False(s.Fall());
        Assert.Equal(new Position(500, 8), s.Position);

        cave.SettledSand.Add(s.Position); // Add sand to cave
        //    0 "......+...",
        //    1 "..........",
        //    2 "..........",
        //    3 "..........",
        //    4 "....#...##",
        //    5 "....#...#.",
        //    6 "..###...#.",
        //    7 "........#.",
        //    8 "......o.#.",
        //    9 "#########.",
        //       4     5
        //       9     0
        //       4567890123
        s = new (new Position(500, 7), cave);
        Assert.True(s.Fall());
        // Sand moves DownLeft
        Assert.Equal(new Position(499, 8), s.Position);
        Assert.False(s.Fall());

        cave.SettledSand.Add(s.Position); // Add sand to cave
        //    0 "......+...",
        //    1 "..........",
        //    2 "..........",
        //    3 "..........",
        //    4 "....#...##",
        //    5 "....#...#.",
        //    6 "..###...#.",
        //    7 "........#.",
        //    8 ".....oo.#.",
        //    9 "#########.",
        //       4     5
        //       9     0
        //       4567890123
        s = new (new Position(500, 7), cave);
        Assert.True(s.Fall());
        // Sand moves DownRight
        Assert.Equal(new Position(501, 8), s.Position);
        Assert.False(s.Fall());

        cave.SettledSand.Add(s.Position);
        //    0 "......+...",
        //    1 "..........",
        //    2 "..........",
        //    3 "..........",
        //    4 "....#...##",
        //    5 "....#...#.",
        //    6 "..###...#.",
        //    7 "........#.",
        //    8 ".....ooo#.",
        //    9 "#########.",
        //       4     5
        //       9     0
        //       4567890123
        s = new (new Position(500, 7), cave);
        Assert.False(s.Fall());

        cave.SettledSand.Add(s.Position); // Add sand to cave
        //    0 "......+...",
        //    1 "..........",
        //    2 "..........",
        //    3 "..........",
        //    4 "....#...##",
        //    5 "....#...#.",
        //    6 "..###...#.",
        //    7 "......o.#.",
        //    8 ".....ooo#.",
        //    9 "#########.",
        //       4     5
        //       9     0
        //       4567890123
        s = new (new Position(500, 6), cave);
        Assert.True(s.Fall());
        // Sand moves DownLeft
        Assert.Equal(new Position(499, 7), s.Position);
        Assert.True(s.Fall());
        // Sand moves DownLeft
        Assert.Equal(new Position(498, 8), s.Position);
        Assert.False(s.Fall());

        //    0 "......+...",
        //    1 "..........",
        //    2 "..........",
        //    3 "..........",
        //    4 "....#...##",
        //    5 "....#...#.",
        //    6 "..###...#.",
        //    7 "......o.#.",
        //    8 "....oooo#.",
        //    9 "#########.",
        //       4     5
        //       9     0
        //       4567890123

    }
}