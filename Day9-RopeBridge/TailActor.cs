public class TailActor : Actor
{

    public Actor Head { get; }
    public TailActor? Tail { get; }

    public TailActor(Actor parent)
    {
        this.Head = parent;
    }

    public TailActor(Actor parent, int length) : this(parent)
    {
        if (length > 1)
        {
            this.Tail = new TailActor(this, length - 1);
        }
    }

    public void Follow()
    {
        if (IsAdjacent()) return;
        int rDist = Math.Sign(Head.Row - this.Row);
        int cDist = Math.Sign(Head.Col - this.Col);
        this.SetPosition(this.Row + rDist, this.Col + cDist);
        this.Tail?.Follow();
    }

    private bool IsAdjacent()
    {
        int rDist = Math.Abs(Head.Row - this.Row);
        int cDist = Math.Abs(Head.Col - this.Col);
        int dist = rDist + cDist;
        return dist < 2 || (rDist == 1 && cDist == 1) || (rDist == 1 && cDist == 1);
    }

}