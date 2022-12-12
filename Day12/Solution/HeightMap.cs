public record HeightMap(int[,] Map, Position Start, HashSet<(int, int)> End)
{

    public HeightMap(int[,] Map, Position Start, Position End) : this(Map, Start, new HashSet<(int, int)>() { End.AsPair })  {}

    public void DisplayMap()
    {
        for (int row = 0; row < Map.GetLength(0); row++)
        {
            for (int col = 0; col < Map.GetLength(1); col++)
            {
                Console.Write($"{Map[row, col], 3}");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public bool IsInBounds(Position p)
    {
        return p.Row >= 0 && 
               p.Row < Map.GetLength(0) && 
               p.Col >= 0 && 
               p.Col < Map.GetLength(1);
    }

    public static bool CheckHeights(int fromHeight, int toHeight)
    {
        return toHeight <= fromHeight + 1;
    }

    public HashSet<Position> FindNeighborsUp(Position p)
    {
        return p.Neighbors.Where(IsInBounds)
                          .Where(n => CheckHeights(Map[p.Row, p.Col], Map[n.Row, n.Col]))
                          .ToHashSet();
    }

    public HashSet<Position> FindNeighborsDown(Position p)
    {
        return p.Neighbors.Where(IsInBounds)
                          .Where(n => CheckHeights(Map[n.Row, n.Col], Map[p.Row, p.Col]))
                          .ToHashSet();
    }

    public static HeightMap Parse(string[] rows)
    {
        return Parse(rows, 'S', 'E');
    }

    public static HeightMap Parse(string[] rows, char startChar, char endChar)
    {
        int[,] heightMap = new int[rows.Length, rows[0].Length];
        Position start = null!;
        HashSet<(int, int)> end = new ();
        for (int row = 0; row < rows.Length; row++)
        {
            for (int col = 0; col < rows[0].Length; col++)
            {
                char ch = rows[row][col];
                start = ch == startChar ? new Position(row, col, null) : start;
                if (ch == endChar)
                {
                    end.Add((row, col));
                }
                heightMap[row, col] = CharHeight(ch);
            }
        }
        return new HeightMap(heightMap, start, end);
    }

    public static int CharHeight(char ch)
    {
        return ch switch
        {
            'S' => (int)'a' -'a',
            'E' => (int)'z' - 'a',
            _ => (int)ch - 'a',
        };
    }
}