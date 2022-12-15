namespace Tests;

public class RangeTest
{
    [Fact]
    public void TestRangeIntersects()
    {
        //  012345678901234567890
        //0 ####
        //1   #
        //2    ###########
        //3            ###
        //4                ######
        //5                ###
        Range[] ranges = new []{
            new Range(0, 3),
            new Range(2, 2),
            new Range(3, 13),
            new Range(10, 13),
            new Range(15, 20),
            new Range(15, 17)
        };
        
        Assert.True(ranges[0].Intersects(ranges[1]));
        Range expected = new Range(0, 3);
        Range result = ranges[0].Merge(ranges[1]);
        Assert.Equal(expected, result);

        Assert.True(result.Intersects(ranges[2]));
        expected = new Range(0, 13);
        result = result.Merge(ranges[2]);
        Assert.Equal(expected, result);

        Assert.True(result.Intersects(ranges[3]));
        expected = new Range(0, 13);
        result = result.Merge(ranges[3]);
        Assert.Equal(expected, result);

        Assert.False(result.Intersects(ranges[4]));
        
        result = new Range(15, 20);
        Assert.True(result.Intersects(ranges[5]));
        expected = new Range(15, 20);
        result = result.Merge(ranges[5]);
        Assert.Equal(expected, result);

    }
}