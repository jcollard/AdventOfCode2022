using System.Text;

public record CubeSolver(Cube Cube, Queue<Move> Moves)
{
    public Position StartPosition
    {
        get
        {
            for (int col = 0; col < Cube.F1.Board.Data[0].Length; col++)
            {
                if (Cube.F1.Board.Data[0][col] == '.')
                {
                    return new Position(0, col);
                }
            }
            throw new Exception("No valid starting position.");
        }
    }

    private Position? _position = null;
    public Position Position { 
        get
        {
            if(_position == null)
            {
                _position = StartPosition;
            }
            return _position;
        }
        private set
        {
            _position = value;
        } 
    }
    public Facing Facing { get; private set; } = Facing.East;

    public Face Face { get; private set; } = Cube.F1;

    public bool Step()
    {
        if (Moves.Count == 0)
        {
            return false;
        }
        (Face face, Position p, Facing f) = Moves.Dequeue().Perform(Position, Facing, Face);
        if (face == Face && p == Position && f == Facing)
        {
            while (Moves.Count > 0 && Moves.Peek() == new Step())
            {
                Moves.Dequeue();
            }
            return true;
        }
        Face = face;
        Position = p;
        Facing = f;
        return true;
    }

    public Position GlobalPosition()
    {
        int top = Face.ID switch {
            1 => 0,
            2 => 150,
            3 => 0,
            4 => 100,
            5 => 50,
            6 => 150,
            _ => throw new Exception("Ooops"),
        };
        int left = Face.ID switch {
            1 => 100,
            2 => 0,
            3 => 50,
            4 => 50,
            5 => 50,
            6 => 0,
            _ => throw new Exception("Ooops"),
        };
        return new Position(Position.Row + top, Position.Col + left);
    }

    public override string ToString()
    {
        StringBuilder b = new ();
        b.Append($"Face {Face.ID}\n");
        int r = 0;
        foreach (char[] row in Face.Board.Data)
        {
            int c = 0;
            foreach (char ch in row)
            {
                if (Position == new Position(r, c))
                {
                    char f = Facing switch {
                        Facing.North => '^',
                        Facing.East => '>',
                        Facing.South => 'v',
                        Facing.West => '<',
                    };
                    b.Append(f);
                }
                else
                {
                    b.Append(ch);
                }
                c++;
            }
            b.Append('\n');
            r++;
        }
        return b.ToString().TrimEnd();
    }

}