// See https://aka.ms/new-console-template for more information
string[] rows = File.ReadAllLines("input.txt");
Solver.IntToSnafu(353);
Console.WriteLine("SNAFU\t\t\tDecimal\t\t\tSNAFU");
long sum = 0;
foreach (string r in rows)
{
    long d = Solver.SnafuToInt(r);
    sum += d;
    string back = Solver.IntToSnafu(d);
    Console.WriteLine($"{r}\t\t\t{d}\t\t\t{back}");
}
Console.WriteLine($"Sum: {sum} => {Solver.IntToSnafu(sum)}");