using System.Text;

public record Solver(Board Board, Queue<Move> Moves)
{
    public Position StartPosition
    {
        get
        {
            for (int col = 0; col < Board.Data[0].Length; col++)
            {
                if (Board.Data[0][col] == '.')
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

    public bool Step()
    {
        if (Moves.Count == 0)
        {
            return false;
        }
        (Position newP, Facing newF) = Moves.Dequeue().Perform(Position, Facing, Board);
        this.Position = newP;
        this.Facing = newF;
        return true;
    }

    public override string ToString()
    {
        StringBuilder b = new ();
        int r = 0;
        foreach (char[] row in Board.Data)
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