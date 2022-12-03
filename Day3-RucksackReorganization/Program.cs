string[] sacks = File.ReadAllLines(args[0]);

Part2();

void Part2()
{
    List<List<string>> groups = ToSackGroups(sacks);
    int sum = 0;
    foreach (List<string> group in groups)
    {
        
        List<HashSet<char>> sets = new ();
        foreach (string sack in group)
        {
            sets.Add(sack.ToCharArray().ToHashSet());
        }
        
        char match = sets.Aggregate((agg, next) => agg.Intersect(next).ToHashSet()).First();  
        
        HashSet<char> agg = sets[0];
        for (int i = 1; i < sets.Count; i++)
        {
            HashSet<char> next = sets[i];
            agg.Intersect(next).ToHashSet();
        }

        int value = char.IsUpper(match) ? match - 'A' + 27 : match - 'a' + 1;
        sum += value;
    }
    Console.WriteLine(sum);
}



List<List<string>> ToSackGroups(string[] sacks)
{
    List<List<string>> groups = new ();
    List<string> current = new ();
    foreach(string sack in sacks)
    {
        current.Add(sack);
        if (current.Count == 3)
        {
            groups.Add(current);
            current = new ();            
        }
    }
    return groups;
}

void Part1()
{
    string valueIx = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    int sum = 0;
    foreach (string sack in sacks)
    {
        HashSet<char> sack0 = sack.Substring(0, sack.Length / 2).ToCharArray().ToHashSet();
        HashSet<char> sack1 = sack.Substring(sack.Length / 2, sack.Length / 2).ToCharArray().ToHashSet();
        char match = sack0.Intersect(sack1).First();
        int value = char.IsUpper(match) ? match - 'A' + 27 : match - 'a' + 1;
        sum += value;
    }
    Console.WriteLine(sum);
}