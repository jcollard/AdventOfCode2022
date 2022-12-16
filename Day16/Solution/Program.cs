string[] rows = File.ReadAllLines("input.txt");
Cave cave = Cave.Parse(rows);
// Console.WriteLine(cave);
cave.BuildAllShortestPaths();
// cave.PrintTravelTimes();
ExplorerWithElephant e = new (cave);
int mostPressure = e.Explore(26);
Console.WriteLine($"Best pressure was {mostPressure}.");