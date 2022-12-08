public static class HeightMap
{
    public static int[,] ParseHeightMap(string[] rows)
    {
        int[,] heightMap = new int[rows.Length, rows.Length];
        for (int row = 0; row < rows.Length; row++)
        {
            for (int col = 0; col < rows[row].Length; col++)
            {
                heightMap[row, col] = int.Parse($"{rows[row][col]}");
            }
        }
        return heightMap;
    }

    public static bool IsVisible(int[,] heightMap, int row, int col)
    {
        return IsVisibleNorth(heightMap, row, col) ||
               IsVisibleEast(heightMap, row, col) || 
               IsVisibleSouth(heightMap, row, col) || 
               IsVisibleWest(heightMap, row, col);
    }

    public static bool IsVisibleEast(int[,] heightMap, int row, int col)
    {
        int height = heightMap[row, col];
        int size = heightMap.GetLength(1);
        for (int c = col + 1; c < size; c++)
        {
            int other = heightMap[row, c];
            if (other >= height)
            {
                return false;
            }
        }
        return true;
    }

    public static bool IsVisibleWest(int[,] heightMap, int row, int col)
    {
        int height = heightMap[row, col];
        for (int c = col - 1; c >= 0; c--)
        {
            int other = heightMap[row, c];
            if (other >= height)
            {
                return false;
            }
        }
        return true;
    }

    public static bool IsVisibleNorth(int[,] heightMap, int row, int col)
    {
        int height = heightMap[row, col];
        for (int r = row - 1; r >= 0; r--)
        {
            int other = heightMap[r, col];
            if (other >= height)
            {
                return false;
            }
        }
        return true;
    }

    public static bool IsVisibleSouth(int[,] heightMap, int row, int col)
    {
        int height = heightMap[row, col];
        int size = heightMap.GetLength(0);
        for (int r = row + 1; r < size; r++)
        {
            int other = heightMap[r, col];
            if (other >= height)
            {
                return false;
            }
        }
        return true;
    }

}