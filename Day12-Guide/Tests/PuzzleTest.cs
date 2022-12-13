namespace Tests;

public class PuzzleTest
{
    [Fact(Timeout = 5000)]
    public void TestParse()
    {
        string[] rows = {
            "Sabqponm",
            "abcryxxl",
            "accszExk",
            "acctuvwj",
            "abdefghi"
        };

        Puzzle result = Puzzle.Parse(rows, 'S', 'E');
        Assert.Equal(new Position(0, 0), result.Start);
        Assert.Equal(new Position(2, 5), result.End);
        // a b c d e f g h i j  k  l  m  n  o  p  q  r  s  t  u  v  w  x  y  z
        // 0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25
        int[,] expected = {
         //   S  a  b   q   p   o   n   m
            { 0, 0, 1, 16, 15, 14, 13, 12},
         //   a  b  c   r   y   x   x   l
            { 0, 1, 2, 17, 24, 23, 23, 11},
         //   a  c  c   s   z   E   x   k
            { 0, 2, 2, 18, 25, 25, 23, 10},            
         //   a  c  c   t   u   v   w   j
            { 0, 2, 2, 19, 20, 21, 22,  9},
         //   a  b  d   e   f   g   h   i
            { 0, 1, 3,  4,  5,  6,  7,  8},
        };
        Assert.Equal(expected, result.Terrain.Heights);
    }
}