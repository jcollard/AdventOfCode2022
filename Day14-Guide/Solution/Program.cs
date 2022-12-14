string[] rows = File.ReadAllLines("input.txt");
Cave ofWonders = Cave.Parse(rows);
do
{
    // Console.Clear();
    // ofWonders.Print();
    // Thread.Sleep(50);
} 
while (ofWonders.Step());
Console.WriteLine($"Saaaaaand... {ofWonders.SettledSand.Count}");
