public class CircularLinkedList
{
    public Node Head { get; set; } = null!;
    public Node Tail { get; set; } = null!;

    public int Size { get; private set; } = 0;
    
    public Node Append(long v)
    {
        Node n = new (v);
        Size++;

        if (Head == null)
        {
            Head = n;
            Tail = n;
            n.Next = n;
            n.Prev = n;
            return n;
        }

        Tail.Next = n;
        n.Prev = Tail;
        Head.Prev = n;
        n.Next = Head;
        Tail = n;
        return n;
    }

    public int IndexOf(int val) => Nodes.Select(n => n.Value).ToList().IndexOf(val);

    public long ValueAt(int ix) => Nodes[ix % Size].Value;

    public void Mix()
    {
        List<Node> nodes = Nodes;
        foreach (Node n in nodes)
        {
            Mix(n);
        }
    }

    public void Mix(Node n)
    {
        long shift = n.Value % ((long)Size - 1L);
        while (shift != 0)
        {
            Action<Node> Shift = shift > 0 ? ShiftRight : ShiftLeft;
            Shift.Invoke(n);
            shift -= Math.Sign(shift);
        }
    }

    public void ShiftRight(Node n)
    {
        if (n == Head)
        {
            Head = n.Next;
        }
        else if (n == Tail)
        {
            Tail = n.Prev;
        }
        else if (n.Next == Tail)
        {
            // Wrap
            Head = n;
        }
        
        Node next = n.Next;
        Remove(n);
        InsertBetween(n, next, next.Next);
    }

    public void ShiftLeft(Node n)
    {
        if (n == Head)
        {
            Head = n.Next;
        }
        else if (n == Tail)
        {
            Tail = n.Prev;
        }
        else if (n.Prev == Head)
        {
            Tail = n;
        }
        Node prev = n.Prev;

        Remove(n);
        InsertBetween(n, prev.Prev, prev);
    }

    public void Remove(Node n)
    {
        Node prev = n.Prev;
        Node next = n.Next;
        prev.Next = next;
        next.Prev = prev;
    }

    public void InsertBetween(Node n, Node prev, Node next)
    {
        prev.Next = n;
        next.Prev = n;
        n.Next = next;
        n.Prev = prev;
    }

    

    public List<Node> Nodes 
    {
        get
        {
            List<Node> nodes = new ();
            Node current = Head;
            while (current != Tail)
            {
                nodes.Add(current);
                current = current.Next;
            }
            nodes.Add(current);
            return nodes;
        }
    }

    public override string ToString() => "[" + string.Join(", ", Nodes) + "]";
    

    public static CircularLinkedList Parse(string[] rows, long key = 1)
    {
        CircularLinkedList list = new ();
        foreach(string row in rows)
        {
            list.Append(long.Parse(row) * key);
        }
        return list;
    }

}

public record Node(long Value)
{
    public Node Prev { get; set; } = null!;
    public Node Next { get; set; } = null!;

    public override string ToString()
    {
        return Value.ToString();
    }
}