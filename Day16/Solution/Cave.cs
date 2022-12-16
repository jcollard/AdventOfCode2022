using System.Text;

public record Cave
{
    public const int INF = 1_000_000;

    public Dictionary<string, Location> Nodes = new ();
    // public Dictionary<string, int> FlowRates = new();
    // public Dictionary<string, List<string>> Edges = new();
    // public Dictionary<string, int> Identifier = new ();
    public Dictionary<(string Start, string End), int> TravelTime = new();
    
    public int Benefit(int timeRemaining, string room, ValveState state)
    {
        return state.IsOn(Nodes[room]) ? 0 : timeRemaining * Nodes[room].FlowRate;
    }

    public void BuildAllShortestPaths()
    {
        if (TravelTime.Count > 0)
        {
            return;
        }
        string[] nodes = Nodes.Keys.ToArray();
        foreach (string node in nodes)
        {
            foreach (string other in nodes)
            {
                if (node == other)
                {
                    TravelTime[(node, node)] = 0;
                }
                else if (Nodes[node].Edges.Contains(other))
                {
                    TravelTime[(node, other)] = 1;
                }
                else
                {
                    TravelTime[(node, other)] = INF;
                }
            }
        }

        foreach (string alt in nodes)
        {
            foreach (string start in nodes)
            {
                foreach (string dest in nodes)
                {
                    int currentSpeed = TravelTime[(start, dest)];
                    int alternatePath = TravelTime[(start, alt)] + TravelTime[(alt, dest)];
                    TravelTime[(start, dest)] = Math.Min(currentSpeed, alternatePath);
                }
            }
        }
    }

    public void PrintTravelTimes()
    {
        string[] nodes = Nodes.Keys.ToArray();
        Console.WriteLine("   | " + string.Join(" | ", nodes) + " |");
        foreach (string from in nodes)
        {
            Console.Write($"{from} ");
            foreach (string to in nodes)
            {
                int time = TravelTime[(from, to)];
                string val = time == INF ? "**" : $"{time,2}";
                Console.Write($"| {val} "); 
            }
            Console.WriteLine("|");
        }
    }

    public void ParseNode(string row, int id)
    {
        // Valve TU has flow rate=0; tunnels lead to valves XG, ID
        string[] tokens = row.Split("; ");
        string[] nodeFlow = tokens[0]
            .Replace("Valve ", "")
            .Replace(" has flow rate=", " ")
            .Split(" ");
        List<string> edges = tokens[1]
            .Replace("tunnels lead to valves ", "")
            .Replace("tunnel leads to valve ", "")
            .Split(", ")
            .ToList();
        Location node = new (id, int.Parse(nodeFlow[1]), edges);
        Nodes[nodeFlow[0]] = node;
    }

    public static Cave Parse(string[] rows)
    {
        Cave cave = new();
        int id = 0;
        foreach (string row in rows)
        {
            cave.ParseNode(row, id++);
        }
        return cave;
    }

    public override string ToString()
    {
        StringBuilder builder = new();
        builder.Append("Cave\n{");
        foreach (string node in Nodes.Keys)
        {
            builder.Append($"  {node} ({Nodes[node].FlowRate} => [{string.Join(", ", Nodes[node].Edges)}]\n");
        }
        builder.Append('}');
        return builder.ToString();
    }

}