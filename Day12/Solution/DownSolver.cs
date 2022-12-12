public record DownSolver(HeightMap Map) : Solver(Map)
{

    public override HashSet<Position> FindNeighbors(Position from, HashSet<(int, int)> visited)
    {
        return Map.FindNeighborsDown(from).Where(n => !visited.Contains(n.AsPair)).ToHashSet();
    }

}