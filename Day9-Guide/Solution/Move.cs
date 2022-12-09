public record Move(int X, int Y)
{
    public readonly static Move RIGHT = new Move(1, 0);
    public readonly static Move LEFT = new Move(-1, 0);
    public readonly static Move UP = new Move(0, 1);
    public readonly static Move DOWN = new Move(0, -1);
    public static List<Move> Parse(string[] input)
    {
        List<Move> moves = new();
        foreach (string row in input)
        {
            string[] tokens = row.Split();
            int times = int.Parse(tokens[1]);
            for (int i = 0; i < times; i++)
            {
                Move m = tokens[0] switch
                {
                    "R" => RIGHT,
                    "L" => LEFT,
                    "U" => UP,
                    "D" => DOWN,
                    _ => throw new Exception($"Could not parse row to Move: {row}")
                };
                moves.Add(m);
            }
        }
        return moves;
    }
}
