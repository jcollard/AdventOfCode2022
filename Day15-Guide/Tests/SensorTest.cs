namespace Tests;

public class SensorTest
{

    [Fact]
    public void TestRangeAtY()
    {
        Sensor sensor = Sensor.Parse("Sensor at x=8, y=7: closest beacon is at x=2, y=10");
        Assert.Equal(9, sensor.Radius);
        
        Range expected = new Range(-1, 17);
        Assert.Equal(expected, sensor.RangeAtY(7));

        expected = new Range(0, 16);
        Assert.Equal(expected, sensor.RangeAtY(6));

        expected = new Range(1, 15);
        Assert.Equal(expected, sensor.RangeAtY(5));

        expected = new Range(2, 14);
        Assert.Equal(expected, sensor.RangeAtY(4));

        expected = new Range(3, 13);
        Assert.Equal(expected, sensor.RangeAtY(3));

        expected = new Range(4, 12);
        Assert.Equal(expected, sensor.RangeAtY(2));

        expected = new Range(5, 11);
        Assert.Equal(expected, sensor.RangeAtY(1));

        expected = new Range(6, 10);
        Assert.Equal(expected, sensor.RangeAtY(0));

        expected = new Range(7, 9);
        Assert.Equal(expected, sensor.RangeAtY(-1));

        expected = new Range(8, 8);
        Assert.Equal(expected, sensor.RangeAtY(-2));
    }

    [Fact]
    public void TestParse()
    {
        Sensor result = Sensor.Parse("Sensor at x=2, y=18: closest beacon is at x=-2, y=15");
        Sensor expected = new Sensor(2, 18, new Position(-2, 15));
        Assert.Equal(expected, result);

        result = Sensor.Parse("Sensor at x=9, y=16: closest beacon is at x=10, y=16");
        expected = new Sensor(9, 16, new Position(10, 16));
        Assert.Equal(expected, result);

        
    }
}