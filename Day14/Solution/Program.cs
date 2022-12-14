string[] rows = File.ReadAllLines("input.txt");
Cave ofWonders = Cave.Parse(rows);
do
{
    // Console.Clear();
    // ofWonders.PrintWindow(new Position(0, 493), new Position(10, 504));
    // Thread.Sleep(10);
} 
while (ofWonders.DropSand());
Console.WriteLine($"Saaaaaand... {ofWonders.SandCount}");
