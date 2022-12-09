public class Actor
{
    public int Row { get; private set; }
    public int Col { get; private set; }
    public (int Row, int Col) Position => (Row, Col);
    public HashSet<(int, int)> Visited = new ();

    public Actor() : this(0, 0) {}

    public Actor(int startRow, int startCol)
    {
        SetPosition(startRow, startCol);
    }
    
    public void SetPosition(int row, int col)
    {
        this.Row = row;
        this.Col = col;
        this.Visited.Add((row, col));
    }
}