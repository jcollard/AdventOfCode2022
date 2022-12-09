string[] rows = File.ReadAllLines("example.txt");
List<Move> moves = Move.Parse(rows);
Position head = new Position(0, 0);
Position tail = new Position(0, 0);
HashSet<Position> positions = new();
positions.Add(tail);
foreach (Move move in moves)
{
    head = Position.Move(head, move);
    tail = Position.Follow(head, tail);
    positions.Add(tail);
}
Console.WriteLine($"The tail visited {positions.Count} unique positions.");