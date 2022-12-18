string[] rows = File.ReadAllLines("input.txt");

Part1();
Part2();

void Part1()
{
    Solver solver = Solver.Parse(rows);
    Console.WriteLine($"Part 1: {solver.ExposedFaces()}");
}

void Part2()
{
    Solver solver = Solver.Parse(rows);
    HashSet<Position> filled = solver.Fill();
    Console.WriteLine($"Fill contains {filled.Count} cubes.");
    int exposed = solver.FacesTouchingFill(filled);
    Console.WriteLine($"Part 2: {exposed}");
}