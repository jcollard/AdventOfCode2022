public abstract record Expr
{
    public abstract long Eval(Dictionary<string, Expr> Lookup);
    public abstract Expr Simplify(Dictionary<string, Expr> Lookup);

    public static Dictionary<string, Expr> Parse(string[] rows)
    {
        Dictionary<string, Expr> lookup = new ();
        foreach(string row in rows)
        {
            string[] tokens = row.Split(":");
            string key = tokens[0].Trim();
            string expr = tokens[1].Trim();
            lookup[key] = ParseExpr(expr);
        }
        return lookup;
    }

    public static Expr ParseExpr(string expr)
    {
        if (long.TryParse(expr, out long value))
        {
            return new Val(value);
        }
        string[] tokens = expr.Split();
        Expr left = new Var(tokens[0].Trim());
        string op = tokens[1].Trim();
        Expr right = new Var(tokens[2].Trim());
        return new BinOp(left, right, op[0]);
    }

};

public record Var(string Key) : Expr
{
    public override long Eval(Dictionary<string, Expr> Lookup) => Lookup[Key].Eval(Lookup);
    public override Expr Simplify(Dictionary<string, Expr> Lookup)
    {
        if(Lookup.TryGetValue(Key, out Expr? expr))
        {
            return expr.Simplify(Lookup);
        }
        else
        {
            return this;
        }
    }

    public override string ToString() => Key;
}

public record Val(long Value) : Expr
{
    public override long Eval(Dictionary<string, Expr> Lookup) => Value;
    public override Expr Simplify(Dictionary<string, Expr> Lookup) => this;

    public override string ToString() => Value.ToString();
}

public record BinOp(Expr Left, Expr Right, char op) : Expr
{
    public override long Eval(Dictionary<string, Expr> Lookup)
    {
        return op switch {
            '+' => Left.Eval(Lookup) + Right.Eval(Lookup),
            '*' => Left.Eval(Lookup) * Right.Eval(Lookup),
            '-' => Left.Eval(Lookup) - Right.Eval(Lookup),
            '/' => Left.Eval(Lookup) / Right.Eval(Lookup),
            _ => throw new Exception($"Cannot eval BinOp({op})"),
        };
    }

    public override Expr Simplify(Dictionary<string, Expr> Lookup)
    {
        Expr left = Left.Simplify(Lookup);
        Expr right = Right.Simplify(Lookup);
        return (left, right) switch 
        {
            (Val l, Val r) => new Val(new BinOp(l, r, op).Eval(Lookup)),
            _ => new BinOp(left, right, op)
        };
    }

    public override string ToString() => $"({Left} {op} {Right})";
}