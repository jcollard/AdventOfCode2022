public record Solver(List<int> List)
{
    public List<int> Indices = Enumerable.Range(0, List.Count).ToList();
    
    public int Get(int ix) => List[ix % List.Count];

    public void Solve()
    {
        // Console.WriteLine(string.Join(", ", List));
        // Console.Write("Indices: ");
        // Console.WriteLine(string.Join(", ", Indices));
        for (int i = 0; i < Indices.Count; i++)
        {
            int fromIx = Indices[i];
            int toIx = NextIndex(fromIx);
            // Console.WriteLine($"Moving {List[fromIx]} @ {fromIx} to {toIx}");

            // Update the actual List
            Move(fromIx, toIx);
            Indices[i] = toIx;
            // Console.WriteLine(string.Join(", ", List));
            // Console.Write("Indices: ");
            // Console.WriteLine(string.Join(", ", Indices));
            // Console.ReadLine();
        }
    }

    public void Move(int fromIx, int toIx)
    {
        if (fromIx == toIx)
        {
            return;
        }
        //  [a, b, [c], d, e, f, g]
        //   0  1   2   3  4  5  6
        //  [a, b, d, e, [c], f, g]
        //   0  1  2  3   4   5  6

        //  [*a*, b, c, d] // fromIx = 0, toIx = 1
        //    0   1  2  3
        //  [b, *a*, c, d]
        //   0   1   2  3
        //

         //  [a, *b*, c, d] // fromIx = 1, toIx = 0
        //    0   1  2  3
        //  [*b*, a, c, d]
        //   0   1   2  3
        //

        // Updates the actual list
        int val = List[fromIx];
        List.RemoveAt(fromIx);
        List.Insert(toIx, val);

        // Update my Indices
        if (toIx > fromIx)
        {
            // Subtract 1
            for (int i = fromIx + 1; i <= toIx; i++)
            {
                int indexOf = Indices.IndexOf(i);
                Indices[indexOf]--;
            }
        }
        else
        {
            // Increment
            for (int i = toIx; i < fromIx; i++)
            {
                int indexOf = Indices.IndexOf(i);
                Indices[indexOf]++;
            }
        }

    }

    // 0 1 2 3 4 5 6 7
    // 1 - 3 = -2
    // 

    public int NextIndex(int ix)
    {
        // a -2 b c d e f
        // 0  1 2 3 4 5 6
        // -2 + 1 = -1 => 6
        // -1 % 7 = -1
        // -1 + 7 = 6;
        int val = List[ix];
        int nextIx = (val + ix) % List.Count;
        if ((val + ix) > 0 && (val + ix) < List.Count)
        {
            return nextIx;
        }
        else if ((val + ix) <= 0)
        {
            nextIx = (val - 1 + ix) % List.Count;
            return (nextIx + List.Count) % List.Count;
        }
        else // (val + ix) >= List.Count
        {
            nextIx = (val + 1 + ix) % List.Count;
            return (nextIx + List.Count) % List.Count;
        }
        
    }

    public static Solver Parse(string[] rows) => new (rows.Select(int.Parse).ToList());

}