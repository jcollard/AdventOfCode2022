namespace Tests;

public class PacketTest
{

    [Fact]
    public void TestEmptyListToString()
    {
        List<object> empty = new();
        Assert.Equal("[]", Packet.ListToString(empty));
    }

    [Fact]
    public void TestSingletonListToString()
    {
        List<object> singleton = new() { 1 };
        Assert.Equal("[1]", Packet.ListToString(singleton));
    }

    [Fact]
    public void TestHomogenousListToString()
    {
        List<object> homogenous = new() { 1, 2, 3, 4 };
        Assert.Equal("[1,2,3,4]", Packet.ListToString(homogenous));
    }

    [Fact]
    public void TestMixedListToString()
    {
        List<object> mixed = new()
        {
            1,
            new List<object> () { 2, 3, 4 },
            5,
            new List<object> () { 6, 7 },
        };
        Assert.Equal("[1,[2,3,4],5,[6,7]]", Packet.ListToString(mixed));
    }

    [Fact]
    public void TestNextedListToString()
    {
        List<object> nested = new()
        {
            new List<object> ()
            {
                1,
                new List<object> { 2, 3 }
            },
            new List<object> ()
            {
                new List<object> { 4, 5 },
                new List<object>
                {
                    new List<object> { 7 },
                    new List<object> { 8 },
                },
            }
        };
        Assert.Equal("[[1,[2,3]],[[4,5],[[7],[8]]]]", Packet.ListToString(nested));
    }

    [Fact]
    public void TestCompareInt()
    {
        Assert.Equal(0, Packet.CompareElements(100, 100));
        Assert.Equal(-1, Packet.CompareElements(50, 75));
        Assert.Equal(1, Packet.CompareElements(360, -50));
    }

    [Fact]
    public void FirstPairTest()
    {
        // [1,1,3,1,1] vs [1,1,5,1,1]
        List<object> p0 = ListParser.Parse("[1,1,3,1,1]");
        List<object> p1 = ListParser.Parse("[1,1,5,1,1]");
        Assert.Equal(-1, Packet.CompareElements(p0, p1));
    }

    [Fact(Timeout = 5000)]
    public void SecondPairTest()
    {
        // [[1],[2,3,4]] vs [[1],4]
        List<object> p0 = ListParser.Parse("[[2,3,4]]");
        List<object> p1 = ListParser.Parse("[4]");
        Assert.Equal(-1, Packet.CompareElements(p0, p1));

        // List<object> p0 = ListParser.Parse("[[1],[2,3,4]]");
        // List<object> p1 = ListParser.Parse("[[1],4]");
        // Assert.Equal(-1, Packet.CompareElements(p0, p1));
    }

    [Fact(Timeout = 5000)]
    public void ThirdPairTest()
    {
        List<object> p0 = ListParser.Parse("[9]");
        List<object> p1 = ListParser.Parse("[[8,7,6]]");
        Assert.Equal(1, Packet.CompareElements(p0, p1));
    }

    [Fact(Timeout = 5000)]
    public void FourthPairTest()
    {
        List<object> p0 = ListParser.Parse("[[4,4],4,4]");
        List<object> p1 = ListParser.Parse("[[4,4],4,4,4]");
        Assert.Equal(-1, Packet.CompareElements(p0, p1));
    }

    [Fact(Timeout = 5000)]
    public void FifthPairTest()
    {
        List<object> p0 = ListParser.Parse("[7,7,7,7]");
        List<object> p1 = ListParser.Parse("[7,7,7]");
        Assert.Equal(1, Packet.CompareElements(p0, p1));
    }

    [Fact(Timeout = 5000)]
    public void SixthPairTest()
    {
        List<object> p0 = ListParser.Parse("[]");
        List<object> p1 = ListParser.Parse("[3]");
        Assert.Equal(-1, Packet.CompareElements(p0, p1));
    }

    [Fact(Timeout = 5000)]
    public void SeventhPairTest()
    {
        List<object> p0 = ListParser.Parse("[[[]]]");
        List<object> p1 = ListParser.Parse("[[]]");
        Assert.Equal(1, Packet.CompareElements(p0, p1));
    }

    [Fact(Timeout = 5000)]
    public void EigthPairTest()
    {
        List<object> p0 = ListParser.Parse("[1,[2,[3,[4,[5,6,7]]]],8,9]");
        List<object> p1 = ListParser.Parse("[1,[2,[3,[4,[5,6,0]]]],8,9]");
        Assert.Equal(1, Packet.CompareElements(p0, p1));
    }

}