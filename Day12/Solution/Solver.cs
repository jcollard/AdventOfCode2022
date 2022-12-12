public record Solver(HeightMap Map)
{
    public Position? Solve()
    {
        HashSet<(int Row, int Col)> visited = new ();
        Queue<Position> toVisit = new ();
        toVisit.Enqueue(Map.Start);
        while (toVisit.Count > 0)
        {
            Position? end = Step(toVisit, visited);
            if (end != null)
            {
                return end;
            }
        }
        
        return null;
    }

    public Position? Step(Queue<Position> toVisit, HashSet<(int, int)> visited)
    {
        Position p = toVisit.Dequeue();
        if (p.AsPair == Map.End.AsPair)
        {
            return p;
        }
        visited.Add(p.AsPair);
        HashSet<Position> neighbors = FindNeighbors(p, visited);
        foreach (Position n in neighbors)
        {
            if (visited.Contains(n.AsPair)) continue;
            visited.Add(n.AsPair);
            toVisit.Enqueue(n);
        }
        return null;
    }

    public HashSet<Position> FindNeighbors(Position from, HashSet<(int, int)> visited)
    {
        return Map.FindNeighbors(from).Where(n => !visited.Contains(n.AsPair)).ToHashSet();
    }

}