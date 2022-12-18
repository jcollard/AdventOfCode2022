public record Solver(HashSet<Position> Cubes)
{
    public int FacesTouchingFill(HashSet<Position> Filled)
    {
        int exposed = 0;
        foreach (Position p in Cubes)
        {
            foreach (Position n in p.Neighbors)
            {
                if (!Filled.Contains(n))
                {
                    exposed++;
                }
            }
        }
        return exposed;
    }

    public HashSet<Position> Fill()
    {
        BoundingBox3D box = BoundingBox3D.Find(Cubes).Pad(1);
        Position corner = new (box.MinX, box.MinY, box.MinZ);
        HashSet<Position> visited = new () { corner };
        Queue<Position> toVisit = new ();
        toVisit.Enqueue(corner);
        while (toVisit.Count > 0)
        {
            Position p = toVisit.Dequeue();
            foreach (Position n in p.Neighbors)
            {
                if (Cubes.Contains(n)) continue;
                if (!box.Contains(n)) continue;
                if (visited.Contains(n)) continue;
                visited.Add(n);
                toVisit.Enqueue(n);
            }
        }
        return visited;
    }

    public int ExposedFaces()
    {
        int exposed = 0;
        foreach (Position p in Cubes)
        {
            foreach (Position n in p.Neighbors)
            {
                if (!Cubes.Contains(n))
                {
                    exposed++;
                }
            }
        }
        return exposed;
    }

    public static Solver Parse(string[] rows)
    {
        HashSet<Position> cubes = new ();
        foreach (string row in rows)
        {
            int[] pos = row.Split(",").Select(int.Parse).ToArray();
            cubes.Add(new Position(pos[0], pos[1], pos[2]));
        }
        return new Solver(cubes);
    }
}