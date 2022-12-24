
using System.Text;

public record Board(List<Blizzard> Blizzards, int Width, int Height)
{
    private Dictionary<Position, List<Blizzard>> _walls = null!; 
    public int TimeSteps { get; set; } = 0;
    public Dictionary<Position, List<Blizzard>> Walls
    {
        get 
        {
            if (_walls == null)
            {
                _walls = new ();
                foreach (Blizzard b in Blizzards)
                {
                    if (!_walls.ContainsKey(b.Position))
                    {
                        _walls[b.Position] = new ();
                    }
                    _walls[b.Position].Add(b);
                }
            }
            return _walls;
        }
    }

    public bool InBounds(Position p) => p.Row >= 0 && p.Col >= 0 && p.Row < Height && p.Col < Width;
    public bool IsOpen(Position p) => !Walls.ContainsKey(p);

    public static Dictionary<int, Board> MemoizedBoards = new ();

    public Board Step()
    {
        if (MemoizedBoards.TryGetValue(TimeSteps + 1, out Board? next))
        {
            return next;
        }
        List<Blizzard> blizzards = new ();
        foreach (Blizzard b in Blizzards)
        {
            Blizzard n = b.Next();
            // if (n.Symbol != '#')
            // {
            //     Console.WriteLine($"{b} => {n}");
            // }
            blizzards.Add(n);
        }
        Board nextBoard = this with { Blizzards = blizzards, _walls = null!, TimeSteps = TimeSteps + 1 };
        MemoizedBoards[nextBoard.TimeSteps] = nextBoard;
        return nextBoard;
    }

    public override string ToString() => ToString(new Position(-5, -5));

    public string ToString(Position Explorer)
    {
        StringBuilder b = new ();
        for (int r = 0; r < Height; r++)
        {
            for (int c = 0; c < Width; c++)
            {
                Position p = new (r, c);
                if (Walls.TryGetValue(p, out List<Blizzard>? blizzards) && blizzards != null)
                {
                    string ch = blizzards.Count > 1 ? blizzards.Count.ToString() : blizzards[0].Symbol.ToString();
                    b.Append(ch);
                }
                else if (p == Explorer)
                {
                    b.Append('E');
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

    public static Board Parse(string[] rows)
    {
        int width = rows[0].Length;
        int height = rows.Length;
        List<Blizzard> blizzards = new ();
        for (int r = 0; r < height; r++)
        {
            for (int c = 0; c < width; c++)
            {
                char ch = rows[r][c];
                if (ch == '.') continue;
                blizzards.Add(Parse(ch, new Position(r, c), width, height));
            }
        }
        return new Board(blizzards, width, height);
    }

    public static Blizzard Parse(char ch, Position p, int width, int height)
    {
        return ch switch
        {
            '>' => new Blizzard(p, East, ch),
            '<' => new Blizzard(p, West, ch),
            '^' => new Blizzard(p, North, ch),
            'v' => new Blizzard(p, South, ch),
            '#' => new Blizzard(p, Wall, ch),
            _ => throw new Exception($"Found unknown entity {ch}"),
        };

        #region  LocalFunctions
        Position Wall(Position p) => p;
        Position East(Position p) => p.E.Wrap(width, height);
        Position West(Position p) => p.W.Wrap(width, height);
        Position North(Position p) => p.N.Wrap(width, height);
        Position South(Position p) => p.S.Wrap(width, height);
        #endregion
    }

}