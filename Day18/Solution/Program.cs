string[] rows = File.ReadAllLines("input.txt");
Part1();
void Part1()
{
    Solver solver = Solver.Parse(rows);
    Console.WriteLine(solver.ExposedFaces());
}