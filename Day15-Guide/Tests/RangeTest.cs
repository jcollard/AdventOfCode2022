namespace Tests;

public class RangeTest
{
    [Fact]
    public void TestContains()
    {
        Range range = new Range(-3, 3);
        Assert.True(range.Contains(-3));
        Assert.True(range.Contains(-2));
        Assert.True(range.Contains(-1));
        Assert.True(range.Contains(0));
        Assert.True(range.Contains(1));
        Assert.True(range.Contains(2));
        Assert.True(range.Contains(3));

        Assert.False(range.Contains(-4));
        Assert.False(range.Contains(4));
    }

    [Fact]
    public void TestMergeSingleton()
    {
        Range r0 = new Range(0, 0);
        Range r1 = new Range(1, 1);
        Assert.True(Range.IsContinuous(r0, r1));
        
        Range merged = r0.MergeWith(r1);
        Range expected = new Range(0, 1);
        Assert.Equal(expected, merged);
        
        merged = r1.MergeWith(r0);
        Assert.Equal(expected, merged);
    }

    [Fact]
    public void TestMergeWithIntersection()
    {
        Range r0 = new Range(-3, 3);
        Range r1 = new Range(3, 6);
        Assert.True(Range.IsContinuous(r0, r1));
        
        Range merged = r0.MergeWith(r1);
        Range expected = new Range(-3, 6);
        Assert.Equal(expected, merged);
        
        merged = r1.MergeWith(r0);
        Assert.Equal(expected, merged);
    }

    [Fact]
    public void TestMergeWithNeighbors()
    {

        Range r0 = new Range(-10, 0);
        Range r1 = new Range(1, 10);
        Assert.True(Range.IsContinuous(r0, r1));

        Range merged = r0.MergeWith(r1);
        Range expected = new Range(-10, 10);
        Assert.Equal(expected, merged);

        merged = r1.MergeWith(r0);
        Assert.Equal(expected, merged);
    }

    [Fact]
    public void TestMergeWithGap()
    {
        Range r0 = new Range(-5, 0);
        Range r1 = new Range(2, 5);
        Assert.False(Range.IsContinuous(r0, r1));

        r0 = new Range(5, 5);
        r1 = new Range(7, 7);
        Assert.False(Range.IsContinuous(r0, r1));
    }

    [Fact]
    public void TestMergeAll()
    {
         //  012345678901234567890
        //0 ####
        //1   #
        //2    ###########
        //3            ###
        //4                ######
        //5                ###
        List<Range> toMerge = new (){
            new Range(0, 3),
            new Range(2, 2),
            new Range(3, 13),
            new Range(10, 13),
            new Range(15, 20),
            new Range(15, 17)
        };
        List<Range> result = Range.MergeAll(toMerge);
        List<Range> expected = new ()
        {
            new Range(0, 13),
            new Range(15, 20)
        };
        Assert.Equal(expected, result);
    }
}