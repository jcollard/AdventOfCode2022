namespace Tests;

public class SensorGridTest
{

    [Fact]
    public void TestParse()
    {
        string[] rows = new[]
        {
            "Sensor at x=2, y=18: closest beacon is at x=-2, y=15",
            "Sensor at x=9, y=16: closest beacon is at x=10, y=16",
            "Sensor at x=13, y=2: closest beacon is at x=15, y=3"
        };

        SensorGrid result = SensorGrid.Parse(rows);
        List<Sensor> expected = new ()
        {
            new Sensor(2, 18, new Position(-2, 15)),
            new Sensor(9, 16, new Position(10, 16)),
            new Sensor(13, 2, new Position(15, 3)),
        };
        Assert.Equal(expected, result.Sensors);
    }

    [Fact]
    public void TestDistinctRangesAtY()
    {
        string[] rows = new[]
        {
            "Sensor at x=2, y=18: closest beacon is at x=-2, y=15",
            "Sensor at x=9, y=16: closest beacon is at x=10, y=16",
            "Sensor at x=13, y=2: closest beacon is at x=15, y=3",
            "Sensor at x=12, y=14: closest beacon is at x=10, y=16",
            "Sensor at x=10, y=20: closest beacon is at x=10, y=16",
            "Sensor at x=14, y=17: closest beacon is at x=10, y=16",
            "Sensor at x=8, y=7: closest beacon is at x=2, y=10",
            "Sensor at x=2, y=0: closest beacon is at x=2, y=10",
            "Sensor at x=0, y=11: closest beacon is at x=2, y=10",
            "Sensor at x=20, y=14: closest beacon is at x=25, y=17",
            "Sensor at x=17, y=20: closest beacon is at x=21, y=22",
            "Sensor at x=16, y=7: closest beacon is at x=15, y=3",
            "Sensor at x=14, y=3: closest beacon is at x=15, y=3",
            "Sensor at x=20, y=1: closest beacon is at x=15, y=3"
        };
        SensorGrid example = SensorGrid.Parse(rows);

        List<Range> result = example.DistinctRangesAtY(9);
        List<Range> row9 = new ()
        {
            new Range(-1, 23)
        };
        Assert.Equal(row9, result);

        result = example.DistinctRangesAtY(10);
        List<Range> row10 = new ()
        {
            new Range(-2, 24)
        };
        Assert.Equal(row10, result);

        result = example.DistinctRangesAtY(11);
        List<Range> row11 = new ()
        {
            new Range(-3, 13),
            new Range(15, 25),
        };
        Assert.Equal(row11, result);
    }

    [Fact]
    public void TestOccupiedSpaces()
    {
        string[] rows = new[]
        {
            "Sensor at x=2, y=18: closest beacon is at x=-2, y=15",
            "Sensor at x=9, y=16: closest beacon is at x=10, y=16",
            "Sensor at x=13, y=2: closest beacon is at x=15, y=3",
            "Sensor at x=12, y=14: closest beacon is at x=10, y=16",
            "Sensor at x=10, y=20: closest beacon is at x=10, y=16",
            "Sensor at x=14, y=17: closest beacon is at x=10, y=16",
            "Sensor at x=8, y=7: closest beacon is at x=2, y=10",
            "Sensor at x=2, y=0: closest beacon is at x=2, y=10",
            "Sensor at x=0, y=11: closest beacon is at x=2, y=10",
            "Sensor at x=20, y=14: closest beacon is at x=25, y=17",
            "Sensor at x=17, y=20: closest beacon is at x=21, y=22",
            "Sensor at x=16, y=7: closest beacon is at x=15, y=3",
            "Sensor at x=14, y=3: closest beacon is at x=15, y=3",
            "Sensor at x=20, y=1: closest beacon is at x=15, y=3"
        };
        SensorGrid example = SensorGrid.Parse(rows);

        Assert.Equal(0, example.OccupiedSpacesAtY(-1));
        Assert.Equal(1, example.OccupiedSpacesAtY(0));
        Assert.Equal(1, example.OccupiedSpacesAtY(1));
        Assert.Equal(2, example.OccupiedSpacesAtY(3));
    }

}