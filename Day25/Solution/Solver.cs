using System.Text;

public static class Solver
{
    public static long SnafuToInt(string snafu)
    {
        long total = 0;
        for (int pos = 0; pos < snafu.Length; pos++)
        {
            char ch = snafu[snafu.Length - 1 - pos];
            total += SnafuToInt(ch, pos);
        }
        return total;
    }

    // SNAFU   DECIMAL
    // 0     |       0
    // 1     |       1
    // 2     |       2
    // 1=    |       3
    // 1-    |       4
    // 10    |       5
    // 
    // 15 => 0
    // 3 => =
    public static string IntToSnafu(long value)
    {
        if (value == 0) return "0";
        List<char> digits = new ();
        while (value > 0)
        {
            // Console.WriteLine($"Value: {value}");
            long digit = value % 5;
            // Console.WriteLine($"Digit: {digit}");
            (char ch, int diff) = digit switch 
            {
                0 => ('0', 0),
                1 => ('1', 0),
                2 => ('2', 0),
                3 => ('=', 2),
                4 => ('-', 1),
            };
            // Console.WriteLine($"Ch: {ch}");
            // Console.WriteLine($"Diff: {diff}");
            digits.Add(ch);
            value = ((value + diff) / 5);
            // Console.WriteLine($"Remaining: {value}");
            // Console.WriteLine($"Thus far: {string.Join("", digits.Reverse<char>())}");
            // Console.ReadLine();
        }
        digits.Reverse();
        return string.Join("", digits);
    }
    

    public static long SnafuToInt(char ch, int pos)
    {
        return ch switch {
            '=' => -2 * (long)(Math.Pow(5, pos)),
            '-' => -1 * (long)(Math.Pow(5, pos)),
            '0' => 0,
            '1' => 1 * (long)(Math.Pow(5, pos)),
            '2' => 2 * (long)(Math.Pow(5, pos)),
        };
    }
}