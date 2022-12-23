using System.Text;

public record Grid(HashSet<Position> Occupied)
{

    private Queue<Rule> _rules = null!;
    public Queue<Rule> Rules 
    { 
        get
        {
            if (_rules == null)
            {
                _rules = new ();
                _rules.Enqueue(Rule.MoveNorth);
                _rules.Enqueue(Rule.MoveSouth);
                _rules.Enqueue(Rule.MoveWest);
                _rules.Enqueue(Rule.MoveEast);
            }
            return _rules;
        }
        private set => _rules = value;
    }

    public Position TopLeft 
    {
        get
        {
            Position topLeft = new (int.MaxValue, int.MaxValue);
            foreach (Position p in Occupied)
            {
                topLeft = new Position(Math.Min(topLeft.Row, p.Row), Math.Min(topLeft.Col, p.Col));
            }
            return topLeft;
        }
    }

    public Position BottomRight 
    {
        get
        {
            Position botRight = new (int.MinValue, int.MinValue);
            foreach (Position p in Occupied)
            {
                botRight = new Position(Math.Max(botRight.Row, p.Row), Math.Max(botRight.Col, p.Col));
            }
            return botRight;
        }
    }

    public int EmptyTiles 
    {
        get
        {
            int width = BottomRight.Col - TopLeft.Col + 1;
            int height = BottomRight.Row - TopLeft.Row + 1;
            return width * height - Occupied.Count;
        }
    }

    public bool Step()
    {
        List<(Position from, Position to)> potentialMovers = FindMovers();
        HashSet<(Position from, Position to)> moves = FindMoves(potentialMovers);
        if (moves.Count == 0)
        {
            return false;
        }
        // HashSet<Position> newOccupied = Occupied.ToHashSet();
        foreach ((Position from, Position to) in moves)
        {
            // Console.WriteLine($"{from} => {to}");
            Occupied.Remove(from);
            Occupied.Add(to);
        }
        // Rotate rules
        Rules.Enqueue(Rules.Dequeue());
        return true;
    }

    public List<(Position, Position)> FindMovers()
    {
        List<(Position, Position)> movers = new ();
        foreach (Position elf in Occupied)
        {
            if(!elf.Neighbors.Where(Occupied.Contains).Any()) continue;
            foreach (Rule r in Rules)
            {
                if (r.CanMove(elf, Occupied))
                {
                    movers.Add((elf, r.Target.Invoke(elf)));
                    break;
                }
            }
        }
        return movers;
    }

    public HashSet<(Position, Position)> FindMoves(List<(Position, Position)> movers)
    {
        Dictionary<Position, List<Position>> targetPositions = new ();
        foreach ((Position from, Position to) in movers)
        {
            if (!targetPositions.ContainsKey(to))
            {
                targetPositions[to] = new () { from };
            }
            else
            {
                targetPositions[to].Add(from);
            }
        }
        return targetPositions
            .Where(kvp => kvp.Value.Count == 1)
            .Select(kvp => (kvp.Value[0], kvp.Key))
            .ToHashSet();
    }

    public override string ToString()
    {
        StringBuilder b = new ();
        for (int r = TopLeft.Row; r <= BottomRight.Row; r++)
        {
            for (int c = TopLeft.Col; c <= BottomRight.Col; c++)
            {
                if (Occupied.Contains(new Position(r, c)))
                {
                    b.Append('#');
                }
                else
                {
                    b.Append('.');
                }
            }
            b.Append('\n');
        }
        return b.ToString().TrimEnd();
    }

    public static Grid Parse(string[] rows)
    {
        HashSet<Position> occupied = new ();
        for (int r = 0; r < rows.Length; r++)
        {
            for (int c = 0; c < rows[r].Length; c++)
            {
                if (rows[r][c] == '#')
                {
                    occupied.Add(new Position(r, c));
                }
            }
        }
        return new Grid(occupied);
    }

}

public record Rule(Func<Position, Position> Target, params Func<Position, Position>[] ToCheck)
{
    
    public static readonly Rule MoveNorth = new (
        p => p.N,
        p => p.N,
        p => p.NE,
        p => p.NW
    );

    public static readonly Rule MoveSouth = new (
        p => p.S,
        p => p.S,
        p => p.SE,
        p => p.SW
    );

    public static readonly Rule MoveWest = new (
        p => p.W,
        p => p.W,
        p => p.NW,
        p => p.SW
    );

    public static readonly Rule MoveEast = new (
        p => p.E,
        p => p.E,
        p => p.NE,
        p => p.SE
    );

    public bool CanMove(Position from, HashSet<Position> occupied)
    {
        return !ToCheck
            .Select(f => f.Invoke(from))
            .Where(occupied.Contains)
            .Any();
    }
}