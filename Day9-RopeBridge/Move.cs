public record Move(int Row, int Col)
{
    public static List<Move> ParseMoves(string[] rows)
    {
        List<Move> moves = new ();
        foreach (string row in rows)
        {
            string[] tokens = row.Split();
            int times = int.Parse(tokens[1]);
            for (int i = 0; i < times; i++)
            {
                Move move = tokens[0] switch {
                    "R" => new Move(0, 1),
                    "L" => new Move(0, -1),
                    "U" => new Move(-1, 0),
                    "D" => new Move(1, 0),
                    _ => throw new Exception("Something terrible happened!")
                };
                moves.Add(move);
            }
        }
        return moves;
    }
}