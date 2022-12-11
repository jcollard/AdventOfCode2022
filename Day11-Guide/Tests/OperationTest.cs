namespace Tests;

public class OperationTest
{
    [Fact]
    public void TestAddition()
    {
        Operation monkey1 = new Operation("old + 6");

        int result = monkey1.Apply(54);
        Assert.Equal(20, result);

        result = monkey1.Apply(65);
        Assert.Equal(23, result);

        Operation oldMonkey = new Operation("old + old");
        
        result = oldMonkey.Apply(15);
        Assert.Equal(10, result);

        result = oldMonkey.Apply(30);
        Assert.Equal(20, result);
    }

    [Fact]
    public void TestMultiplication()
    {
        Operation monkey0 = new Operation("old * 19");
        int result = monkey0.Apply(79);
        Assert.Equal(500, result);

        result = monkey0.Apply(98);
        Assert.Equal(620, result);

        Operation monkey2 = new Operation("old * old");
        result = monkey2.Apply(79);
        Assert.Equal(2080, result);
        
        result = monkey2.Apply(60);
        Assert.Equal(1200, result);
    }
}