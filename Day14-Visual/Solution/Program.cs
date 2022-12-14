string[] rows = File.ReadAllLines("example.txt");
Cave ofWonders = Cave.Parse(rows);
ofWonders.Animate();
// do
// {
//     // Console.Clear();
//     // ofWonders.Print();
//     // Thread.Sleep(50);
// } 
// while (ofWonders.Step());
// Console.WriteLine($"Saaaaaand... {ofWonders.SettledSand.Count}");
