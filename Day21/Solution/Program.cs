// See https://aka.ms/new-console-template for more information
string[] rows = File.ReadAllLines("input.txt");
Part2();
void Part1()
{
    Dictionary<string, Expr> vars = Expr.Parse(rows);
    long result = vars["root"].Eval(vars);
    Console.WriteLine(result);
}

void Part2()
{
    Dictionary<string, Expr> vars = Expr.Parse(rows);
    vars.Remove("humn");
    BinOp root = (BinOp)vars["root"];
    string leftKey = ((Var)root.Left).Key;
    string rightKey = ((Var)root.Right).Key;
    Expr left = vars[leftKey].Simplify(vars);
    Expr right = vars[rightKey].Simplify(vars);
    long rightVal = right.Eval(vars);
    
    Console.WriteLine($"{leftKey}: {left}");
    Console.WriteLine($"{rightKey}: {right}");

    long max = 4_000_000_000_000;
    // long max = 9_223_372_036_854_775_807;
    long min = 3_000_000_000_000;
    long humn = (max - min)/2;
    for (; ; )
    {
        Console.WriteLine($"{min}, {humn}, {max}");
        Dictionary<string, Expr> lookup = new();
        // Console.Write("Enter humn: ");
        // humn = long.Parse(Console.ReadLine()!);
        lookup["humn"] = new Val(humn);
        long result = left.Eval(lookup);
        if (result == rightVal)
        {
            Console.WriteLine($"humn was {humn}");
            break;
        }
        else if (result < rightVal)
        {
            max = humn;
            humn = min + ((max - min) / 2);
            Console.WriteLine($"{result} != \n{rightVal}");
            Console.WriteLine("Too high...");            
        }
        else
        {
            min = humn;
            humn = min + ((max - min) / 2);
            Console.WriteLine($"{result} != \n{rightVal}");
            Console.WriteLine($"Too low...");
        }
        // Console.ReadLine();
    }
    
}