namespace Tests;

public class MonkeyTest
{
    [Fact]
    public void TestParse()
    {
        string monkey0Data = @"
Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3".Trim();

        Monkey monkey0 = Monkey.Parse(monkey0Data);
        List<int> expected = new () {79, 98};
        Assert.Equal(expected, monkey0.Items);
        Assert.Equal(new Operation("old * 19"), monkey0.ItemOperation);
        Assert.Equal(23, monkey0.Divisor);
        Assert.Equal(2, monkey0.TrueIx);
        Assert.Equal(3, monkey0.FalseIx);
    }

    [Fact]
    public void TestParseAll()
    {
        string monkeyData = @"
Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1".Trim();

        
        Monkey monkey0 = new Monkey(new (){ 79, 98 }, new Operation("old * 19"), 23, 2, 3);
        Monkey monkey1 = new Monkey(new (){ 54, 65, 75, 74 }, new Operation("old + 6"), 19, 2, 0);
        Monkey monkey2 = new Monkey(new (){ 79, 60, 97 }, new Operation("old * old"), 13, 1, 3);
        Monkey monkey3 = new Monkey(new (){ 74 }, new Operation("old + 3"), 17, 0, 1);

        List<Monkey> result = Monkey.ParseAll(monkeyData);
        Assert.Equal(monkey0.Items, result[0].Items);
        Assert.Equal(monkey1.Items, result[1].Items);
        Assert.Equal(monkey2.Items, result[2].Items);
        Assert.Equal(monkey3.Items, result[3].Items);

        Assert.Equal(monkey0.ItemOperation, result[0].ItemOperation);
        Assert.Equal(monkey1.ItemOperation, result[1].ItemOperation);
        Assert.Equal(monkey2.ItemOperation, result[2].ItemOperation);
        Assert.Equal(monkey3.ItemOperation, result[3].ItemOperation);

        Assert.Equal(monkey0.Divisor, result[0].Divisor);
        Assert.Equal(monkey1.Divisor, result[1].Divisor);
        Assert.Equal(monkey2.Divisor, result[2].Divisor);
        Assert.Equal(monkey3.Divisor, result[3].Divisor);

        Assert.Equal(monkey0.TrueIx, result[0].TrueIx);
        Assert.Equal(monkey1.TrueIx, result[1].TrueIx);
        Assert.Equal(monkey2.TrueIx, result[2].TrueIx);
        Assert.Equal(monkey3.TrueIx, result[3].TrueIx);

        Assert.Equal(monkey0.FalseIx, result[0].FalseIx);
        Assert.Equal(monkey1.FalseIx, result[1].FalseIx);
        Assert.Equal(monkey2.FalseIx, result[2].FalseIx);
        Assert.Equal(monkey3.FalseIx, result[3].FalseIx);
    }

    [Fact]
    public void TestInspectItems()
    {
        Monkey monkey0 = new Monkey(new (){ 79, 98 }, new Operation("old * 19"), 23, 2, 3);
        Monkey monkey1 = new Monkey(new (){ 54, 65, 75, 74 }, new Operation("old + 6"), 19, 2, 0);
        Monkey monkey2 = new Monkey(new (){ 79, 60, 97 }, new Operation("old * old"), 13, 1, 3);
        Monkey monkey3 = new Monkey(new (){ 74 }, new Operation("old + 3"), 17, 0, 1);
        
        List<Monkey> friends = new () { monkey0, monkey1, monkey2, monkey3 };

        // Monkey 0 throws 500 to Monkey 3
        // Monkey 0 throws 620 to Monkey 3
        monkey0.InspectItems(friends);

        Assert.Empty(monkey0.Items); // No items left
        Assert.Equal(2, monkey0.InspectionCount); // Inspected 2 items

        List<int> expectedItems = new () { 74, 500, 620 }; // Monkey 3 now has 3 items
        Assert.Equal(expectedItems, monkey3.Items);

        monkey1.InspectItems(friends);
        // Monkey 1 throws 20 to monkey 0
        // Monkey 1 throws 23 to monkey 0
        // Monkey 1 throws 27 to monkey 0
        // Monkey 1 throws 26 to monkey 0

        Assert.Empty(monkey1.Items); // No items left
        Assert.Equal(4, monkey1.InspectionCount); // Inspected 4 items

        expectedItems = new () { 20, 23, 27, 26 };
        Assert.Equal(expectedItems, monkey0.Items);

        monkey2.InspectItems(friends);
        // Monkey 2 throws 2080 to Monkey 1
        // Monkey 2 throws 1200 to Monkey 3
        // Monkey 2 throws 3136 to Monkey 3

        Assert.Empty(monkey2.Items); // No items left
        Assert.Equal(3, monkey2.InspectionCount); // Inspected 4 items

        expectedItems = new () { 2080 };
        Assert.Equal(expectedItems, monkey1.Items);
        expectedItems = new () { 74, 500, 620, 1200, 3136 };
        Assert.Equal(expectedItems, monkey3.Items);

        monkey3.InspectItems(friends);
        // Monkey 3 throws 25 is thrown to monkey 1.
        // Monkey 3 throws 167 is thrown to monkey 1.
        // Monkey 3 throws 207 is thrown to monkey 1.
        // Monkey 3 throws 401 is thrown to monkey 1.
        // Monkey 3 throws 1046 is thrown to monkey 1.

        Assert.Empty(monkey3.Items); // No items left
        Assert.Equal(5, monkey3.InspectionCount); // Inspected 4 items

        expectedItems = new () { 2080, 25, 167, 207, 401, 1046 };
        Assert.Equal(expectedItems, monkey1.Items);

    }
}