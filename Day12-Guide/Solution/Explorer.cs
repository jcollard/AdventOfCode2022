public class Explorer
{
    public Terrain Map { get; }
    public Position Start { get; }
    public Queue<Position> ExplorationQueue { get; private set; }
    public int[,] Distances;

    public Explorer(Terrain terrain, Position start)
    {
        this.Map = terrain;
        this.Start = start;
        this.Distances = new int[Map.Rows, Map.Cols];
        
        for (int r = 0; r < Map.Rows; r++)
        {
            for (int c = 0; c < Map.Cols; c++)
            {
                this.Distances[r,c] = -1;
            }
        }

        Distances[Start.Row, Start.Col] = 0;
        ExplorationQueue = new Queue<Position>();
        ExplorationQueue.Enqueue(Start);
    }

    public int DistanceTo(Position p)
    {
        return this.Distances[p.Row, p.Col];
    }

    public bool IsExploring()
    {
        return ExplorationQueue.Count() > 0;
    }

    public Position Explore()
    {
        // If there is nothing left in the ExplorationQueue
        // there is nothing left to do.
        if (ExplorationQueue.Count == 0)
        {
            throw new Exception("Nothing left to explore!");
        }

        // We have not solved the puzzle yet
        Position exploring = ExplorationQueue.Dequeue();
        foreach (Position neighbor in Map.FindNeighbors(exploring))
        {
            // If the neighbor has not yet been explored, we 
            // set the distance and add it to the ExplorationQueue
            if (Distances[neighbor.Row, neighbor.Col] == -1)
            {
                Distances[neighbor.Row, neighbor.Col] = Distances[exploring.Row, exploring.Col] + 1;
                ExplorationQueue.Enqueue(neighbor);
            }
        }
        return exploring;
    }


}