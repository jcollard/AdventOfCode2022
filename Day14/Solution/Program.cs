string[] rows = File.ReadAllLines("input.txt");
Cave ofWonders = Cave.Parse(rows);
do
{
    // Console.Clear();
    // ofWonders.PrintWindow(new Position(0, 480), new Position(13, 510));
    // Thread.Sleep(50);
} 
while (ofWonders.DropSand());
Console.WriteLine($"Saaaaaand... {ofWonders.SandCount}");
