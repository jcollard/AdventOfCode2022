public record Sand
{
    public Position Position { get; private set; }
    public Cave Cave { get; }
    
    public Sand(Position start, Cave cave)
    {
        Position = start;
        Cave = cave;
    }

    public bool Fall()
    {
        if (!Cave.IsOccupied(Position.Down))
        {
            this.Position = Position.Down;
            return true;
        }
        if (!Cave.IsOccupied(Position.DownLeft))
        {
            this.Position = Position.DownLeft;
            return true;
        }
        if (!Cave.IsOccupied(Position.DownRight))
        {
            this.Position = Position.DownRight;
            return true;
        }
        return false;
    }

}