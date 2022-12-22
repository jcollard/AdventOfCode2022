using System.Text;

public abstract record Move
{
    public abstract (Position, Facing) Perform(Position p, Facing f, Board b);

    public static Queue<Move> Parse(string data)
    {
        Queue<Move> moves = new ();
        int ix = 0;
        while (ix < data.Length)
        {
            ix = ParseMoves(moves, data, ix);
        }
        return moves;
    }

    public static int ParseMoves(Queue<Move> moves, string data, int ix)
    {
        (int steps, ix) = ParseInt(data, ix);
        for (int i = 0; i < steps; i++)
        {
            moves.Enqueue(new Step());
        }
        if (ix < data.Length)
        {
            char rotation = data[ix++];
            Move r = rotation switch {
                'R' => new RotateRight(),
                'L' => new RotateLeft(),
                _ => throw new Exception($"Cannot parse rotation from {rotation}"),
            };
            moves.Enqueue(r);
        }
        return ix;
    }

    public static (int num, int ix) ParseInt(string data, int ix)
    {
        StringBuilder b = new ();
        while (ix < data.Length && char.IsDigit(data[ix]))
        {
            b.Append(data[ix++]);
        }
        return (int.Parse(b.ToString()), ix);
    }



}

public record Step : Move
{
    public override (Position, Facing) Perform(Position p, Facing f, Board b)
    {
        Position next = b.NextPosition(p, f);
        return (next, f);
    }
}

public record RotateLeft : Move
{
    public override (Position, Facing) Perform(Position p, Facing f, Board b)
    {
        return f switch
        {
            Facing.North => (p, Facing.West),  
            Facing.West => (p, Facing.South),
            Facing.South => (p, Facing.East),
            Facing.East => (p, Facing.North),
            _ => throw new Exception($"Cannot rotate left with facing {f}"),
        };
    }
}

public record RotateRight : Move
{
    public override (Position, Facing) Perform(Position p, Facing f, Board b)
    {
        return f switch
        {
            Facing.North => (p, Facing.East),  
            Facing.East => (p, Facing.South),
            Facing.South => (p, Facing.West),
            Facing.West => (p, Facing.North),
            _ => throw new Exception($"Cannot rotate right with facing {f}"),
        };
    }
}

public static class FacingUtils
{
    public static int ToInt(this Facing f)
    {
        return f switch {
            Facing.North => 3,
            Facing.East => 0,
            Facing.South => 1,
            Facing.West => 2,
            _ => throw new Exception($"Invalid facing {f}"),
        };
    }
}

public enum Facing
{
    North, East, South, West
}