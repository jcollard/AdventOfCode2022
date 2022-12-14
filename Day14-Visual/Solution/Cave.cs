public record Cave(HashSet<Position> Rocks)
{
    public static readonly Position Origin = new(500, 0);
    public HashSet<Position> SettledSand { get; } = new();

    public (Position TopLeft, Position BottomRight) Bounds = Position.FindBounds(Rocks);

    public bool Step()
    {
        Sand s = new Sand(Origin, this);
        while (s.Fall())
        {
            if (IsAtBottom(s.Position))
            {
                return false;
            }
        }
        SettledSand.Add(s.Position);
        return true;
    }

    public static void DrawSymbol(char ch, Position p, (Position TL, Position BR) Bounds, ConsoleColor color)
    {
        int offX = Bounds.TL.X - 1;
        int offY = Bounds.TL.Y - 1;
        Console.SetCursorPosition(p.X - offX, p.Y - offY);
        Console.ForegroundColor = color;
        Console.Write(ch);
    }

    public void CursorVisible(bool visible)
    {
        try
        {
            Console.CursorVisible = visible;
        }
        catch
        {

        }
    }

    public void Animate(int nextSandDelay = 5, int delay = 25)
    {
        CursorVisible(false);
        Random rng = new Random();
        int next = rng.Next(2, nextSandDelay+1);
        InitPrint();
        Queue<Sand> grains = new();
        int tick = 0;
        while (true)
        {
            HashSet<Position> toClear = new HashSet<Position>();
            if (tick++ % next == 0)
            {
                grains.Enqueue(new Sand(Origin, this));
                next = rng.Next(2, nextSandDelay+1);
            }
            Queue<Sand> nextQueue = new();
            foreach (Sand s in grains)
            {

                DrawSymbol('*', s.Position, Bounds, ConsoleColor.Yellow);
                if (IsAtBottom(s.Position))
                {
                    goto end;
                }

                Position prev = s.Position;
                if (s.Fall())
                {
                    toClear.Add(prev);
                    nextQueue.Enqueue(s);
                }
                else
                {
                    DrawSymbol('o', s.Position, Bounds, ConsoleColor.DarkYellow);
                    SettledSand.Add(s.Position);
                }

            }
            Thread.Sleep(delay);
            grains = nextQueue;
            foreach (Position p in toClear)
            {
                DrawSymbol(' ', p, Bounds, ConsoleColor.Black);                    
            }


        }
    end:

        Console.ResetColor();
        CursorVisible(true);
    }

    public bool IsOccupied(Position p)
    {
        return Rocks.Contains(p) || SettledSand.Contains(p);
    }

    public bool IsAtBottom(Position p)
    {
        return p.Y >= Bounds.BottomRight.Y;
    }

    public static Cave Parse(string[] rows)
    {
        HashSet<Position> occupied = new();
        foreach (string row in rows)
        {
            Position[] positions = row.Split(" -> ").Select(Position.Parse).ToArray();
            for (int i = 0; i < positions.Length - 1; i++)
            {
                occupied.UnionWith(ParseSegment(positions[i], positions[i + 1]));
            }
        }
        return new Cave(occupied);
    }

    public static HashSet<Position> ParseSegment(Position start, Position end)
    {
        HashSet<Position> ps = new();
        ps.Add(start);
        while (start != end)
        {
            int diffX = Math.Sign(end.X - start.X);
            int diffY = Math.Sign(end.Y - start.Y);
            start = new Position(start.X + diffX, start.Y + diffY);
            ps.Add(start);
        }
        return ps;
    }

    public void InitPrint()
    {
        Console.Clear();
        (Position TopLeft, Position BottomRight) = Position.FindBounds(Rocks, 1);
        Console.ForegroundColor = ConsoleColor.Gray;
        for (int row = TopLeft.Y; row <= BottomRight.Y; row++)
        {
            for (int col = TopLeft.X; col <= BottomRight.X; col++)
            {
                Position p = new Position(col, row);
                if (this.IsOccupied(p))
                {
                    try
                    {
                        DrawSymbol('#', p, Bounds, ConsoleColor.Gray);
                    }
                    catch
                    {

                    }
                }
            }
        }
    }

    public void Print(Sand? falling = null)
    {
        (Position TopLeft, Position BottomRight) = Position.FindBounds(Rocks, 1);

        for (int row = TopLeft.Y; row <= BottomRight.Y; row++)
        {
            for (int col = TopLeft.X; col <= BottomRight.X; col++)
            {
                Position p = new Position(col, row);
                if (falling?.Position == p)
                {
                    Console.Write("*");
                }
                else if (SettledSand.Contains(p))
                {
                    Console.Write("o");
                }
                else if (this.IsOccupied(p))
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
    }
}