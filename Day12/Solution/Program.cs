
string[] rows = File.ReadAllLines("input.txt");
HeightMap map = HeightMap.Parse(rows);
// map.DisplayMap();

Solver solver = new Solver(map);
Position? end = solver.Solve();
List<Position> path = end.Path();
Console.WriteLine(path.Count - 1);
// Console.WriteLine(string.Join(", ", path.Select(p => p.AsPair)));