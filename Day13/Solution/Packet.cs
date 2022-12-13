using System.Text;
public abstract record Packet
{
    public int Compare(Packet other)
    {
        return (this, other) switch {
            (PInt p0, PInt p1) => Math.Sign(p0.Val - p1.Val),
            (PInt p0, _) => p0.Promote().Compare(other),
            (_, PInt p1) => this.Compare(p1.Promote()),
            (PList p0, PList p1) => CompareLists(p0.Packets, p1.Packets),
            _ => throw new Exception($"Unexpected compare {this} with {other}"),
        };
    }

    public static int CompareLists(List<Packet> ls, List<Packet> js)
    {
        for (int i = 0; i < ls.Count; i++)
        {
            if (i >= js.Count)
            {
                return 1; // First list is longer than second list
            }
            Packet p0 = ls[i];
            Packet p1 = js[i];
            int diff = p0.Compare(p1);
            if (diff == 0)
            {
                continue;
            }
            return diff;
        }
        return Math.Sign(ls.Count - js.Count);
    }

    public static Packet Parse(string packet)
    {
        Queue<char> data = new Queue<char>();
        packet.ToList().ForEach(data.Enqueue);
        return Parse(data);
        
    }

    private static Packet Parse(Queue<char> data)
    {
        char ch = data.Peek();
        if (char.IsDigit(ch))
        {
            return ParseInt(data);
        }
        else if (ch == '[')
        {
            data.Dequeue();
            return data.Peek() switch 
            {
                ']' => new PList(),
                _ => new PList(ParseList(data, new List<Packet>())),
            };
        }
        else
        {
            throw new Exception($"Invalid type, expected integer or list...");
        }
    }

    private static List<Packet> ParseList(Queue<char> data, List<Packet> container)
    {
        Packet el = Parse(data);  // Parse the next element 
        container.Add(el); // Add it to the container
        char ch = data.Dequeue(); // Check next char
        return ch switch
        {
            ',' => ParseList(data, container),
            ']' => container,
            _ => throw new Exception("Invalid list, expected ',' or ']'"),
        };
    }

    private static PInt ParseInt(Queue<char> data)
    {
        StringBuilder token = new StringBuilder();
        while(data.Count > 0 && char.IsDigit(data.Peek()))
        {
            token.Append(data.Dequeue());
        }
        return new PInt(int.Parse(token.ToString()));
    }
}

public record PList(List<Packet> Packets) : Packet
{

    public PList(params Packet[] packets) : this(packets.ToList()) { }

    public static List<Packet> Parse(char[] data, int ix)
    {
        List<Packet> ls = new List<Packet>();

        return ls;
    }

    public override string ToString()
    {
        return "[" + string.Join(",", Packets) +  "]";
    }
}

public record PInt(int Val) : Packet
{
    public PList Promote()
    {
        return new PList(this);
    }

    public override string ToString()
    {
        return Val.ToString();
    }
}