public record Monkey(List<int> Items,
                     Operation ItemOperation,
                     int Divisor,
                     int TrueIx,
                     int FalseIx)
{
    public int InspectionCount { get; private set; } = 0;

    public void InspectItems(List<Monkey> friends)
    {
        foreach (int item in this.Items)
        {
            int newVal = ItemOperation.Apply(item);
            if (newVal % Divisor == 0)
            {
                friends[TrueIx].Items.Add(newVal);
            }
            else
            {
                friends[FalseIx].Items.Add(newVal);
            }
            InspectionCount++;
        }
        this.Items.Clear();
    }

    public static void ExecuteRound(List<Monkey> friends)
    {
        foreach (Monkey monkey in friends)
        {
            monkey.InspectItems(friends);
        }
    }

    public static List<Monkey> ParseAll(string input)
    {
        List<Monkey> friends = new();
        string[] monkeyData = input.Split("\n\n");
        foreach (string monkey in monkeyData)
        {
            friends.Add(Parse(monkey));
        }
        return friends;
    }

    public static Monkey Parse(string monkeyInfo)
    {
        string[] rows = monkeyInfo.Split("\n");
        List<int> items = ParseStartItems(rows[1]);
        Operation op = Operation.Parse(rows[2]);
        int divisor = ParseLastInt(rows[3]);
        int trueIx = ParseLastInt(rows[4]);
        int falseIx = ParseLastInt(rows[5]);
        return new Monkey(items, op, divisor, trueIx, falseIx);
    }

    public static List<int> ParseStartItems(string row)
    {
        string[] tokens = row.Split(":");
        string[] itemStrings = tokens[1].Split(",");
        List<int> items = new();
        foreach (string item in itemStrings)
        {
            items.Add(int.Parse(item));
        }
        return items;
    }

    public static int ParseLastInt(string row)
    {
        return int.Parse(row.Split()[^1]);
    }
}