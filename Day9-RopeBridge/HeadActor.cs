public class HeadActor : Actor
{
    public TailActor Tail { get; }

    public HeadActor()
    {
        this.Tail = new TailActor(this);
    }

    public HeadActor(int length)
    {
        this.Tail = new TailActor(this, length - 1);
    }
    public void Move(Move move)
    {
        this.SetPosition(this.Row + move.Row, this.Col + move.Col);
        this.Tail.Follow();
    }
}