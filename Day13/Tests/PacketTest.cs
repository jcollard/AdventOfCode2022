namespace Tests;

public class PacketTest
{
    [Fact]
    public void FirstPairTest()
    {
        // [1,1,3,1,1] vs [1,1,5,1,1]
        Packet p0 = Packet.Parse("[1,1,3,1,1]");
        Packet p1 = Packet.Parse("[1,1,5,1,1]");
        Assert.Equal(-1, p0.Compare(p1));
    }

    [Fact(Timeout = 5000)]
    public void SecondPairTest()
    {
        // [[1],[2,3,4]] vs [[1],4]
        Packet p0 = Packet.Parse("[[1],[2,3,4]]");
        Packet p1 = Packet.Parse("[[1],4]");
        Assert.Equal(-1, p0.Compare(p1));
    }

    [Fact(Timeout = 5000)]
    public void ThirdPairTest()
    {
        Packet p0 = Packet.Parse("[9]");
        Packet p1 = Packet.Parse("[[8,7,6]]");
        Assert.Equal(1, p0.Compare(p1));
    }

    [Fact(Timeout = 5000)]
    public void FourthPairTest()
    {
        Packet p0 = Packet.Parse("[[4,4],4,4]");
        Packet p1 = Packet.Parse("[[4,4],4,4,4]");
        Assert.Equal(-1, p0.Compare(p1));
    }

    [Fact(Timeout = 5000)]
    public void FifthPairTest()
    {
        Packet p0 = Packet.Parse("[7,7,7,7]");
        Packet p1 = Packet.Parse("[7,7,7]");
        Assert.Equal(1, p0.Compare(p1));
    }

    [Fact(Timeout = 5000)]
    public void SixthPairTest()
    {
        Packet p0 = Packet.Parse("[]");
        Packet p1 = Packet.Parse("[3]");
        Assert.Equal(-1, p0.Compare(p1));
    }

    [Fact(Timeout = 5000)]
    public void SeventhPairTest()
    {
        Packet p0 = Packet.Parse("[[[]]]");
        Packet p1 = Packet.Parse("[[]]");
        Assert.Equal(1, p0.Compare(p1));
    }

    [Fact(Timeout = 5000)]
    public void EigthPairTest()
    {
        Packet p0 = Packet.Parse("[1,[2,[3,[4,[5,6,7]]]],8,9]");
        Packet p1 = Packet.Parse("[1,[2,[3,[4,[5,6,0]]]],8,9]");
        Assert.Equal(1, p0.Compare(p1));
    }


    [Fact]
    public void TestParse()
    {
        string[] examples = {
            "[1,1,3,1,1]",
            "[1,1,5,1,1]",
            "[[1],[2,3,4]]",
            "[[1],4]",
            "[9]",
            "[[8,7,6]]",
            "[[4,4],4,4]",
            "[[4,4],4,4,4]",
            "[7,7,7,7]",
            "[7,7,7]",
            "[]",
            "[3]",
            "[[[]]]",
            "[[]]",
            "[1,[2,[3,[4,[5,6,7]]]],8,9]",
            "[1,[2,[3,[4,[5,6,0]]]],8,9]",
        };

        Assert.Equal(examples[0], Packet.Parse(examples[0]).ToString());
        Assert.Equal(examples[1], Packet.Parse(examples[1]).ToString());
        Assert.Equal(examples[2], Packet.Parse(examples[2]).ToString());
        Assert.Equal(examples[3], Packet.Parse(examples[3]).ToString());
        Assert.Equal(examples[4], Packet.Parse(examples[4]).ToString());
        Assert.Equal(examples[5], Packet.Parse(examples[5]).ToString());
        Assert.Equal(examples[6], Packet.Parse(examples[6]).ToString());
        Assert.Equal(examples[7], Packet.Parse(examples[7]).ToString());
        Assert.Equal(examples[8], Packet.Parse(examples[8]).ToString());
        Assert.Equal(examples[9], Packet.Parse(examples[9]).ToString());
        Assert.Equal(examples[10], Packet.Parse(examples[10]).ToString());
        Assert.Equal(examples[11], Packet.Parse(examples[11]).ToString());
        Assert.Equal(examples[12], Packet.Parse(examples[12]).ToString());
        Assert.Equal(examples[13], Packet.Parse(examples[13]).ToString());
        Assert.Equal(examples[14], Packet.Parse(examples[14]).ToString());
        Assert.Equal(examples[15], Packet.Parse(examples[15]).ToString());

        Packet[] parsed = examples.Select(Packet.Parse).ToArray();
        string[] result = parsed.Select(p => p.ToString()).ToArray();
        Assert.Equal(examples, result);
    }
}