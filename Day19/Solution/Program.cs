// See https://aka.ms/new-console-template for more information
BluePrint[] rows = File.ReadAllLines("input.txt").Select(BluePrint.Parse).ToArray();
Part2();
void Part2()
{
    List<(int, int)> Solutions = new();
    foreach (BluePrint bp in rows.Take(3))
    {
        Console.WriteLine($"Starting solver for BP #{bp.ID}");
        Solver s = new (bp);
        int solution = s.Solve(32);
        Console.WriteLine($"Best was: {solution} searched {s.Memoized.Count} nodes.");
        Solutions.Add((bp.ID, solution));
    }

    int product = 1;
    foreach ((int id, int solution) in Solutions)
    {
        Console.WriteLine($"{id} => {solution} => Quality Level: {id*solution}");
        product *= solution;
    }
    Console.WriteLine($"Total quality: {product}");
}

void Part1()
{
    List<(int, int)> Solutions = new();
    foreach (BluePrint bp in rows)
    {
        Console.WriteLine($"Starting solver for BP #{bp.ID}");
        Solver s = new (bp);
        int solution = s.Solve(24);
        Console.WriteLine($"Best was: {solution} searched {s.Memoized.Count} nodes.");
        Solutions.Add((bp.ID, solution));
    }

    int sum = 0;
    foreach ((int id, int solution) in Solutions)
    {
        Console.WriteLine($"{id} => {solution} => Quality Level: {id*solution}");
        sum += id*solution;
    }
    Console.WriteLine($"Total quality: {sum}");
}