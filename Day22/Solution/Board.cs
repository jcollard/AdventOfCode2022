using System.Text;

public record Board(char[][] Data)
{

    public Position NextPosition(Position position, Facing facing)
    {
        Position desired = Wrap(position.FromFacing(facing), facing);
        if (IsOpen(desired))
        {
            return desired;
        }
        else if (IsWall(desired))
        {
            return position;
        }
        else
        {
            throw new Exception($"Desired position was not open OR wall, {position} / {facing}");
        }
    }

    public Position Wrap(Position p, Facing f)
    {
        // Console.WriteLine($"Wrap ({p}, {f})");
        if (IsInBounds(p) && IsAir(p))
        {
            // Console.WriteLine("Was in bounds and was air!");
            return Wrap(p.FromFacing(f), f);
        }
        if (!IsInBounds(p))
        {
            // Console.WriteLine("Was not in bounds.");
            Position newP = f switch {
                Facing.East => p with { Col = 0 },
                Facing.West => p with { Col = Data[0].Length - 1 },
                Facing.North => p with { Row = Data.Length - 1 },
                Facing.South => p with { Row = 0 },
                _ => throw new Exception($"Could not move {f}"),
            };
            // Console.WriteLine($"NewP {newP}.");
            return Wrap(newP, f);
        }
        return p;
    }

    public bool IsWall(Position p) => Data[p.Row][p.Col] == '#';
    public bool IsOpen(Position p) => Data[p.Row][p.Col] == '.';
    public bool IsAir(Position p) => Data[p.Row][p.Col] == ' ';

    public bool IsInBounds(Position p){
        return p.Row >= 0 && p.Row < Data.Length &&
               p.Col >= 0 && p.Col < Data[0].Length;
    }

    public static Board Parse(string[] rows)
    {
        int ColLength = rows.Select(r => r.Length).Max();
        char[][] data = new char[rows.Length][];
        for (int ix = 0; ix < data.Length; ix++)
        {
            data[ix] = rows[ix].PadRight(ColLength, ' ').ToCharArray();
        }
        return new Board(data);
    }

    public override string ToString()
    {
        StringBuilder b = new ();
        foreach (char[] row in Data)
        {
            foreach (char ch in row)
            {
                b.Append(ch);
            }
            b.Append('\n');
        }
        return b.ToString().TrimEnd();
    }

}