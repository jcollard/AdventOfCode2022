public record HeightMap(int[,] Map, Position Start, Position End)
{

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

    public HashSet<Position> FindNeighbors(Position p)
    {
        return p.Neighbors.Where(IsInBounds)
                          .Where(n => CheckHeights(Map[p.Row, p.Col], Map[n.Row, n.Col]))
                          .ToHashSet();
    }

    public static HeightMap Parse(string[] rows)
    {
        int[,] heightMap = new int[rows.Length, rows[0].Length];
        Position start = null!;
        Position end = null!;
        for (int row = 0; row < rows.Length; row++)
        {
            for (int col = 0; col < rows[0].Length; col++)
            {
                char ch = rows[row][col];
                start = ch == 'S' ? new Position(row, col, null) : start;
                end = ch == 'E' ? new Position(row, col, null) : end;
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