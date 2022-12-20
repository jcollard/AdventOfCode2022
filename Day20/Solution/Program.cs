// See https://aka.ms/new-console-template for more information

string[] rows = File.ReadAllLines("input.txt");
// Part1();
Part2();
void Part1()
{
    CircularLinkedList list = CircularLinkedList.Parse(rows);
    list.Mix();
    int ix0 = list.IndexOf(0);
    long v0 = list.ValueAt(ix0 + 1000);
    long v1 = list.ValueAt(ix0 + 2000);
    long v2 = list.ValueAt(ix0 + 3000);
    Console.WriteLine($"ix0: {ix0}");
    Console.WriteLine($"v0: {v0}");
    Console.WriteLine($"v1: {v1}");
    Console.WriteLine($"v2: {v2}");
    Console.WriteLine($"sum: {v0 + v1 + v2}");
}

void Part2()
{
    CircularLinkedList list = CircularLinkedList.Parse(rows, 811_589_153L);
    List<Node> nodes = list.Nodes;
    // Console.WriteLine(list);
    for (int i = 0; i < 10; i++)
    {
        foreach (Node n in nodes)
        {
            list.Mix(n);
        }
    }
    // Console.WriteLine(list);
    int ix0 = list.IndexOf(0);
    long v0 = list.ValueAt(ix0 + 1000);
    long v1 = list.ValueAt(ix0 + 2000);
    long v2 = list.ValueAt(ix0 + 3000);
    Console.WriteLine($"ix0: {ix0}");
    Console.WriteLine($"v0: {v0}");
    Console.WriteLine($"v1: {v1}");
    Console.WriteLine($"v2: {v2}");
    Console.WriteLine($"sum: {v0 + v1 + v2}");
}