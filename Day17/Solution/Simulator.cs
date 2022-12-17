public record Simulator(string Input)
{
    public Board Board { get; private set; }
    public IEnumerator<Position> JetStream { get; } = ParseJetStream(Input).GetEnumerator();
    public IEnumerator<Piece> PieceQueue { get; } = Piece.Pieces().GetEnumerator();
    public int SetPieces { get; private set; } = 0;
    
    public void InitBoard()
    {
        if (Board == null)
        {
            PieceQueue.MoveNext();
            Piece first = PieceQueue.Current;
            Piece falling = PieceQueue.Current.Shift(new Position(3, 2));
            Board = new Board(new HashSet<Position>(), falling, 0);
        }
    }

    public void Step()
    {
        StepJet();
        StepFall();
    }

    public Position StepJet()
    {
        InitBoard();
        JetStream.MoveNext();
        Position offset = JetStream.Current;
        
        if (Board.TryShift(offset, out Board update))
        {
            this.Board = update;
        }

        return offset;
    }

    public void StepFall()
    {
        if (Board.TryShift(new Position(-1, 0), out Board update))
        {
            this.Board = update;
        }
        else
        {
            PieceQueue.MoveNext();
            this.Board = this.Board.SetPiece(PieceQueue.Current);
            SetPieces++;
        }
    }
    
    public static IEnumerable<Position> ParseJetStream(string row)
    {
        while(true)
        {
            foreach (char ch in row)
            {
                yield return ch switch 
                {
                    '>' => new Position(0, 1),
                    '<' => new Position(0, -1),
                    _ => throw new Exception($"Cannot parse jet stream char {ch}")
                };
            }
        }
    }
    
}