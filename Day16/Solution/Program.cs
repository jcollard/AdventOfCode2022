string[] rows = File.ReadAllLines("input.txt");
Cave cave = Cave.Parse(rows);
// Console.WriteLine(cave);
cave.BuildAllShortestPaths();
// cave.PrintTravelTimes();
ExplorerWithElephant e = new (cave);
int mostPressure = e.Explore(26);
if (mostPressure != 2382)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Incorrect answer!");
}
Console.WriteLine($"Best pressure was {mostPressure}.");
Console.WriteLine($"Nodes explored: {e.Results.Count}");
Console.WriteLine($"Swaps: {e.Swaps}");
Console.WriteLine($"Cache Hits: {e.CacheHits}");
Console.WriteLine($"Cache Misses: {e.CacheMisses}");