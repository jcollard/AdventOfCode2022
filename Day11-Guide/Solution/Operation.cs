public record Operation(string Expression)
{
    public int Apply(int item)
    {
        string[] tokens = Expression.Split();
        return (tokens[1], tokens[2]) switch
        {
            ("*", "old") => (item * item) / 3,
            ("+", "old") => (item + item) / 3,
            ("*", _) => (item * int.Parse(tokens[2])) / 3,
            ("+", _) => (item + int.Parse(tokens[2])) / 3,
            _ => throw new Exception($"Could not perform Operation: {Expression}"),
        };
    }

    
    public static Operation Parse(string input)
    {
        return new Operation(input.Split("=")[1].Trim());
    }
}