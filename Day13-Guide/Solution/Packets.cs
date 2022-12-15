public static class Packets
{

    public static string ListToString(List<object> objects)
    {
        List<string> asStrings = new();
        foreach (object el in objects)
        {
            string s = el switch
            {
                (List<object> list) => ListToString(list),
                _ => $"{el}",
            };
            asStrings.Add(s);
        }
        return "[" + string.Join(",", asStrings) + "]";
    }

    public static int CompareElements(object first, object second)
    {
        return (first, second) switch
        {
            (int f, int s) => Math.Sign(f - s),
            (int f, List<object> s) => CompareLists(new List<object>() {f}, s),
            (List<object> f, int s) => CompareLists(f, new List<object>() {s}),
            (List<object> f, List<object> s) => CompareLists(f, s),
            _ => throw new Exception($"Could not compare elements {first} vs. {second}."),
        };
    }

    public static int CompareLists(List<object> first, List<object> second)
    {
        int maxIx = Math.Min(first.Count, second.Count);
        for (int ix = 0; ix < maxIx; ix++)
        {
            object el0 = first[ix];
            object el1 = second[ix];
            int diff = CompareElements(el0, el1);
            if (diff < 0)
            {
                return -1;
            }
            else if (diff > 0)
            {
                return 1;
            }
        }
        return Math.Sign(first.Count - second.Count);
    }

}
