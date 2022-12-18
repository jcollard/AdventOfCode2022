public record struct TopRow(int BitMask);
public record struct State(TopRow TopRow, Piece NextPiece, string Moves, int Id);

public record Simulator(string Input)
{
    public Board Board { get; private set; }
    public IEnumerator<JetStream> Stream { get; } = JetStream.Parse(Input).GetEnumerator();
    public IEnumerator<Piece> PieceQueue { get; } = Piece.Pieces().GetEnumerator();
    public int SetPieces { get; private set; } = 0;
    public State LastPieceState { get; private set; }
    public State PieceState { get; private set; }
    public Dictionary<State, State> Edges = new ();
    
    public void InitBoard()
    {
        if (Board == null)
        {
            PieceQueue.MoveNext();
            Piece first = PieceQueue.Current;
            Piece falling = PieceQueue.Current.Shift(new Position(3, 2));
            Board = new Board(new HashSet<Position>(), falling, 0);
            PieceState = new State(Board.TopRow, falling, string.Empty, 0);
        }
    }

    public bool Step()
    {
        StepJet();
        return StepFall();
    }

    public Position StepJet()
    {
        InitBoard();
        Stream.MoveNext();
        JetStream stream = Stream.Current;
        Position offset = stream.Offset;
        char ch = offset == new Position(0, -1) ? '<' : '>';
        PieceState = PieceState with { Moves = PieceState.Moves + ch };
        
        if (Board.TryShift(offset, out Board update))
        {
            this.Board = update;
        }

        return offset;
    }

    public bool StepFall()
    {
        if (Board.TryShift(new Position(-1, 0), out Board update))
        {
            this.Board = update;
        }
        else
        {
            PieceQueue.MoveNext();

            if(CheckForCycles())
            {
                this.Board = this.Board.SetPiece(PieceQueue.Current);
                SetPieces++;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public bool CheckForCycles()
    {
        if (Edges.ContainsKey(PieceState))
        {
            Edges[LastPieceState] = PieceState;
            Console.WriteLine("Cycle detected!");
            // We have been here before!
            // Count pieces in cycle and height gained by this cycle
            List<State> cycle = FindCycle(PieceState);
            
            int heightBeforeCycle = Board.HighestPoint;
            Console.WriteLine("\n\nBefore Cycle\n\n");
            // this.Board.PrintBoard();
            Board withCycle = DropPieces(cycle);
            Console.WriteLine("\n\nWith Cycle\n\n");
            // withCycle.PrintBoard();

            int heightAfterCycle = withCycle.HighestPoint;
            int cycleHeight = heightAfterCycle - heightBeforeCycle;
            
            Console.WriteLine($"Cycle has height {cycleHeight}.");
            Console.WriteLine($"Cycle found with {cycle.Count} rocks.");
            Console.WriteLine($"Height before cycle was {heightBeforeCycle}");
            Console.WriteLine($"Rocks before cycle was {SetPieces}");

            ulong rocksToDrop = 1_000_000_000_000 - (ulong)SetPieces;
            ulong numCycles = rocksToDrop / (ulong)cycle.Count;
            int leftOver = (int)(rocksToDrop % (ulong)cycle.Count);

            Console.WriteLine($"Need to run {numCycles.ToString("N0")} cycles + {leftOver} additional rocks");
            List<State> extraRocks = cycle.Take(leftOver).ToList();
            Board afterExtras = DropPieces(extraRocks);
            int extraHeight = afterExtras.HighestPoint - Board.HighestPoint;
            Console.WriteLine($"Extra height {extraHeight}");
            ulong h = (numCycles * (ulong)cycleHeight) + (ulong)(extraHeight + heightBeforeCycle);
            Console.WriteLine($"Height is {h}");

            // ulong rockCount = (ulong)SetPieces;
            // ulong th = (ulong)Board.HighestPoint;
            // while (rockCount < rocksToDrop)
            // {
            //     th += (ulong)cycleHeight;
            //     rockCount += (ulong)cycle.Count;
            //     Console.WriteLine($"Height at {rockCount} = {th}");
            // }

            ulong f = 1_514_285_714_288;
            return false;
        }
        else if (LastPieceState != null)
        {
            Edges[LastPieceState] = PieceState;
        }
        LastPieceState = PieceState;
        PieceState = new State(Board.TopRow, PieceQueue.Current, string.Empty, Stream.Current.Id);
        return true;
    }

    public Board DropPieces(List<State> pieces)
    {
        Board board = Board;
        foreach(State s in pieces)
        {
            board = DropPiece(board, s);
        }
        return board;
    }

    public Board DropPiece(Board board, State state)
    {
        board = board.SpawnPiece(state.NextPiece);
        foreach (char ch in state.Moves)
        {
            Position p = ch == '<' ? new Position(0, -1) : new Position(0, 1);
            if(board.TryShift(p, out Board update))
            {
                board = update;
            }
            if(board.TryShift(new Position(-1, 0), out update))
            {
                board = update;
            }
        }
        board = board.SetPiece(Piece.J);
        return board;
    }


    public List<State> FindCycle(State start)
    {
        List<State> cycle = new () { start };
        State next = Edges[start];
        while (next != start)
        {
            cycle.Add(next);
            next = Edges[next];
        }
        return cycle;
    }
    
    
    
}