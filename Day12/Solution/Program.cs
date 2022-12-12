
string[] rows = File.ReadAllLines("input.txt");

Part2();

void Part2()
{
    HeightMap map = HeightMap.Parse(rows, 'E', 'a');
    Solver solver = new DownSolver(map);
    Position? end = solver.Solve();
    List<Position> path = end.Path();
    Console.WriteLine(path.Count - 1);
}

void Part1()
{
    HeightMap map = HeightMap.Parse(rows, 'S', 'E');
    Solver solver = new Solver(map);
    Position? end = solver.Solve();
    List<Position> path = end.Path();
    Console.WriteLine(path.Count - 1);
}