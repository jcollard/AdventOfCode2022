public record Sand
{
    public Position Position { get; private set; }
    private Cave _cave;
    
    public Sand(Position start, Cave cave)
    {
        Position = start;
        _cave = cave;
    }

    public bool Fall()
    {
        if (!_cave.IsOccupied(Position.Down))
        {
            this.Position = Position.Down;
            return true;
        }
        if (!_cave.IsOccupied(Position.DownLeft))
        {
            this.Position = Position.DownLeft;
            return true;
        }
        if (!_cave.IsOccupied(Position.DownRight))
        {
            this.Position = Position.DownRight;
            return true;
        }
        return false;
    }

}