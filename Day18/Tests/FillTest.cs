namespace Tests;

public class FillTest
{
    [Fact(Timeout = 5000)]
    public void TestFill()
    {
        HashSet<Position> cubes = new () { new Position(1, 1, 1) };
        Solver solver = new (cubes);
        HashSet<Position> fill = solver.Fill();
        Assert.Equal(26, fill.Count);
        int result = solver.FacesTouchingFill(fill);
        int expected = 6;
        Assert.Equal(expected, result);
    }
}