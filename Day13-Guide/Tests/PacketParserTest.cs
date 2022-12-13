namespace Tests;

public class PacketParserTest
{

    [Fact]
    public void TestParseInt()
    {
        Queue<char> data = ListParser.StringToQueue("689,85]");
        int result = ListParser.ParseInt(data);
        Assert.Equal(689, result);
        Assert.Equal(",85]", string.Join("", data));

        data = ListParser.StringToQueue("85]");
        result = ListParser.ParseInt(data);
        Assert.Equal(85, result);
        Assert.Equal("]", string.Join("", data));
    }

    [Fact]
    public void TestParseEmptyList()
    {
        Queue<char> data = ListParser.StringToQueue("[]");
        List<object> result = ListParser.ParseList(data);
        List<object> expected = new ();
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestParseSingletonList()
    {
        Queue<char> data = ListParser.StringToQueue("[1]");
        List<object> result = ListParser.ParseList(data);
        List<object> expected = new () { 1 };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestParseHomogenousList()
    {
        Queue<char> data = ListParser.StringToQueue("[1,17,6,14]");
        List<object> result = ListParser.ParseList(data);
        List<object> expected = new () { 1, 17, 6, 14 };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestParseMixedList()
    {
        Queue<char> data = ListParser.StringToQueue("[1,[2,3],4,[5,6]]");
        List<object> result = ListParser.ParseList(data);
        List<object> expected = new () 
        { 
            1, 
            new List<object>{ 2, 3 },
            4, 
            new List<object>{5, 6}
        };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestParseNestedList()
    {
        Queue<char> data = ListParser.StringToQueue("[[1,[2,[3]]]]");
        List<object> result = ListParser.ParseList(data);
        List<object> expected = new () 
        { 
            new List<object>()
            {
                1,
                new List<object>()
                {
                    2,
                    new List<object>()
                    {
                        3
                    }
                }
            }
        };
        Assert.Equal(expected, result);
    }
}