public record Puzzle(Terrain Terrain, Position Start, Position End)
{
    public static Puzzle Parse(string[] rows, char startChar, char endChar)
    {
        int[,] heights = new int[rows.Length, rows[0].Length];
        Position start = new Position(0, 0);
        Position end = new Position(0, 0);
        for (int r = 0; r < rows.Length; r++)
        {
            for (int c = 0; c < rows[0].Length; c++)
            {
                char ch = rows[r][c];
                if (ch == startChar)
                {
                    start = new Position(r, c);
                }
                if (ch == endChar)
                {
                    end = new Position(r, c);
                }
                heights[r, c] = CharHeight(ch);
            }
        }
        return new Puzzle(new Terrain(heights), start, end);
    }

    public static int CharHeight(char ch)
    {
        return ch switch
        {
            'E' => 'z' - 'a',
            'S' => 0,
            _ => ch - 'a'
        };
    }
}