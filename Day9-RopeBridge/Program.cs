List<Move> moves = Move.ParseMoves(File.ReadAllLines("input.txt"));
Part2();

void Part1()
{
    HeadActor head = new HeadActor();
    foreach (Move m in moves)
    {
        head.Move(m);
    }
    Console.WriteLine(head.Tail.Visited.Count);
}

void Part2()
{
    HeadActor head = new HeadActor(10);
    foreach (Move m in moves)
    {
        head.Move(m);
    }
    TailActor tail = head.Tail;
    while (tail.Tail != null)
    {
        tail = tail.Tail;
    }
    Console.WriteLine(tail.Visited.Count); 
}