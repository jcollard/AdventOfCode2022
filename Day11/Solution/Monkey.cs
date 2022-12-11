public record Monkey(List<long> StartItems, Func<long, long> Op, int Divisor, int TrueIx, int FalseIx)
{
    private static long NEXT_ID = 0;
    public long ID { get; } = NEXT_ID++;
    private Queue<long> _items = new (StartItems);
    public long InspectionCount { get; private set; } = 0;

    public bool InspectItem(List<Monkey> friends)
    {
        if (this._items.Count == 0)
        {
            return false;
        }
        InspectionCount++;
        long oldVal = this._items.Dequeue();
        long newVal = NewVal(oldVal);
        int ix = newVal % Divisor == 0 ? TrueIx : FalseIx;
        friends[ix]._items.Enqueue(newVal);
        return true;
    }

    protected virtual long NewVal(long oldVal)
    {
        return Op.Invoke(oldVal) / 3;
    }

    public static Monkey Parse(string monkeyInfo)
    {
        string[] rows = monkeyInfo.Split("\n");
        List<long> startItems = rows[1].Split(":")[1].Split(",").Select(long.Parse).ToList();
        Func<long, long> op = ParseOperator(rows[2].Split("=")[1].Trim());
        int divisor = int.Parse(rows[3].Split("by")[1]);
        int trueIx = int.Parse(rows[4].Split("monkey")[1]);
        int falseIx = int.Parse(rows[5].Split("monkey")[1]);
        return new Monkey(startItems, op, divisor, trueIx, falseIx);
    }

    public static Func<long, long> ParseOperator(string input)
    {
        string[] tokens = input.Split();
        return (tokens[1], tokens[2]) switch {
            ("*", "old") => ((old) => old * old),
            ("+", "old") => ((old) => old + old),
            ("*", _) => ((old) => old * long.Parse(tokens[2])),
            ("+", _) => ((old) => old + long.Parse(tokens[2])),
            _ => throw new Exception($"Could not parse operator {input}")
        };
    }


}