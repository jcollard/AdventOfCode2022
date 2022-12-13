string[] rows = File.ReadAllLines("input.txt");
Puzzle puzzle = Puzzle.Parse(rows, 'S', 'E');
Explorer explorer = new Explorer(puzzle.Terrain, puzzle.Start);
while (explorer.IsExploring())
{
    Position currentLocation = explorer.Explore();
    if (currentLocation == puzzle.End)
    {
        Console.WriteLine($"The shortest path is {explorer.DistanceTo(currentLocation)} steps");
        break;
    }
}