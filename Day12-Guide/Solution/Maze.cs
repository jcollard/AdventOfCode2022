public record Maze(int[,] Heights)
{

    public int Rows => Heights.GetLength(0);
    public int Cols => Heights.GetLength(1);

    public List<Position> FindNeighbors(Position pos)
    {
        List<Position> neighbors = new();
        foreach (Position neighbor in pos.Neighbors())
        {
            if (CheckMove(pos, neighbor))
            {
                neighbors.Add(neighbor);
            }
        }
        return neighbors;
    }

    public bool Contains(Position pos)
    {
        return pos.Row >= 0 && pos.Col >= 0 &&
               pos.Row < this.Rows && pos.Col < this.Cols;
    }

    public bool CheckMove(Position from, Position to)
    {
        if (!Contains(from) || !Contains(to))
        {
            return false;
        }
        int toHeight = this.Heights[to.Row, to.Col];
        int fromHeight = this.Heights[from.Row, from.Col];
        return toHeight <= fromHeight + 1;
    }
}