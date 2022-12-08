
string[] rows = File.ReadAllLines("input.txt");

int[,] heightMap = HeightMap(rows);
int SIZE = heightMap.GetLength(0);

Part2();
// PrintHeightMap(heightMap);
void Part2()
{
    Dictionary<(int row, int col), ViewScores> scores = CalculateViewScores();
    int max = scores.Values.Select(s => s.ScenicScore).Max();
    Console.WriteLine($"The max scenic score was {max}");
}

Dictionary<(int row, int col), ViewScores> CalculateViewScores()
{
    Dictionary<(int row, int col), ViewScores> scores = new();
    foreach ((int row, int col) pos in WestToEast())
    {
        scores[pos] = new ViewScores();
    }
    
    Func<int, int, bool> IsFirst = ((int row, int col) => col == 0);
    Action<int, int, int> UpdateScore = ((int row, int col, int val) => scores[(row, col)].West = val);
    PerformUpdates(IsFirst, UpdateScore, WestToEast());

    IsFirst = ((int row, int col) => col == SIZE - 1);
    UpdateScore = ((int row, int col, int val) => scores[(row, col)].East = val);
    PerformUpdates(IsFirst, UpdateScore, EastToWest());

    IsFirst = ((int row, int col) => row == 0);
    UpdateScore = ((int row, int col, int val) => scores[(row, col)].North = val);
    PerformUpdates(IsFirst, UpdateScore, NorthToSouth());

    IsFirst = ((int row, int col) => row == SIZE - 1);
    UpdateScore = ((int row, int col, int val) => scores[(row, col)].South = val);
    PerformUpdates(IsFirst, UpdateScore, SouthToNorth());
    return scores;
}

void PrintScores(Dictionary<(int row, int col), ViewScores> scores)
{
    for (int row = 0; row < SIZE; row++)
    {
        for (int col = 0; col < SIZE; col++)
        {
            Console.Write($"{scores[(row, col)].West, 3}");
        }
        Console.WriteLine();
    }
}


void PerformUpdates(Func<int, int, bool> IsFirst,
                    Action<int, int, int> UpdateScore,
                    IEnumerable<(int row, int col)> order)
{
    Stack<int> prevHeights = new Stack<int>();
    foreach ((int row, int col) in order)
    {
        int treeHeight = heightMap[row, col];
        if (IsFirst(row, col))
        {
            UpdateScore(row, col, 0);
            prevHeights.Clear();
        }
        else
        {
            int view = 0;
            foreach (int otherHeight in prevHeights.ToList())
            {
                view++;
                if (otherHeight >= treeHeight) break;
            }
            UpdateScore(row, col, view);
        }
        prevHeights.Push(treeHeight);
    }
}


IEnumerable<(int row, int col)> WestToEast()
{
    for (int row = 0; row < SIZE; row++)
    {
        for (int col = 0; col < SIZE; col++)
        {
            yield return (row, col);
        }
    }
}

IEnumerable<(int row, int col)> EastToWest()
{
    for (int row = 0; row < SIZE; row++)
    {
        for (int col = SIZE - 1; col >= 0; col--)
        {
            yield return (row, col);
        }
    }
}

IEnumerable<(int row, int col)> NorthToSouth()
{
    for (int col = 0; col < SIZE; col++)
    {
        for (int row = 0; row < SIZE; row++)
        {
            yield return (row, col);

        }
    }
}

IEnumerable<(int row, int col)> SouthToNorth()
{
    for (int col = 0; col < SIZE; col++)
    {
        for (int row = SIZE - 1; row >= 0; row--)
        {
            yield return (row, col);
        }
    }
}

void Part1()
{
    HashSet<(int, int)> visibleTrees = new();
    // Go left to right
    for (int row = 0; row < SIZE; row++)
    {
        // Left to right
        int tallest = int.MinValue;
        for (int col = 0; col < SIZE; col++)
        {
            int current = heightMap[row, col];
            if (current > tallest)
            {
                visibleTrees.Add((row, col));
                tallest = current;
            }
        }

        // Right to left
        tallest = int.MinValue;
        for (int col = SIZE - 1; col >= 0; col--)
        {
            int current = heightMap[row, col];
            if (current > tallest)
            {
                visibleTrees.Add((row, col));
                tallest = current;
            }
        }
    }


    for (int col = 0; col < SIZE; col++)
    {
        // Go Top to Bottom
        int tallest = int.MinValue;
        for (int row = 0; row < SIZE; row++)
        {
            int current = heightMap[row, col];
            if (current > tallest)
            {
                visibleTrees.Add((row, col));
                tallest = current;
            }
        }

        // Go Bottom to Top
        tallest = int.MinValue;
        for (int row = SIZE - 1; row >= 0; row--)
        {
            int current = heightMap[row, col];
            if (current > tallest)
            {
                visibleTrees.Add((row, col));
                tallest = current;
            }
        }
    }

    // Console.WriteLine(string.Join(", ", visibleTrees));
    Console.WriteLine(visibleTrees.Count());
}


void PrintHeightMap(int[,] map)
{
    for (int row = 0; row < map.GetLength(0); row++)
    {
        for (int col = 0; col < map.GetLength(0); col++)
        {
            Console.Write(map[row, col]);
        }
        Console.WriteLine();
    }
}

int[,] HeightMap(string[] rows)
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