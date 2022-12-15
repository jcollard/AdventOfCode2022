string[] rows = File.ReadAllLines("example.txt");
Cave ofWonders = Cave.Parse(rows);
do
{
    Console.Clear();
    Console.WriteLine(ofWonders.PrintWindow(new Position(494, 0), new Position(503, 9)));
    Thread.Sleep(50);
} 
while (ofWonders.DropSand());
Console.WriteLine($"Saaaaaand... {ofWonders.SettledSand.Count}");
