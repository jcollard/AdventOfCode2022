public record JetStream(Position Offset, int Id)
{
    public static IEnumerable<JetStream> Parse(string row)
    {
        while(true)
        {
            int i = 0;
            foreach (char ch in row)
            {
                yield return ch switch 
                {
                    '>' => new JetStream (new Position(0, 1), i++),
                    '<' => new JetStream (new Position(0, -1), i++),
                    _ => throw new Exception($"Cannot parse jet stream char {ch}")
                };
            }
        }
    }
}