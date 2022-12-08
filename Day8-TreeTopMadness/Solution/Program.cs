string[] rows = File.ReadAllLines(args[0]);
int[,] heightMap = HeightMap.ParseHeightMap(rows);
int count = 0;
for (int r = 0; r < heightMap.GetLength(0); r++)
{
    for (int c = 0; c < heightMap.GetLength(1); c++)
    {
        if (HeightMap.IsVisible(heightMap, r, c))
        {
            count++;
        }
    }
}
Console.WriteLine($"There are {count} visible trees.");