public record Position(int X, int Y, int Z)
{
    public Position Up => this with { Y = this.Y + 1 };
    public Position Down => this with { Y = this.Y - 1 };
    public Position Front => this with { Z = this.Z - 1 };
    public Position Back => this with { Z = this.Z + 1 };
    public Position Left => this with { X = this.X - 1 };
    public Position Right => this with { X = this.X + 1 };

    public IEnumerable<Position> Neighbors => new []
    {
        Up, Down, Front, Back, Left, Right
    };

    public override string ToString()
    {
        return $"Position(X = {X}, Y = {Y})";
    }
}