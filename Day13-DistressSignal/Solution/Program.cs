    string data = File.ReadAllText("example.txt");
    string[] pairs = data.Split("\n\n");
    int ix = 1;
    int sum = 0;
    foreach (string pair in pairs)
    {
        string[] packetStrings = pair.Split("\n");
        List<object> p0 = ListParser.Parse(packetStrings[0].Trim());
        List<object> p1 = ListParser.Parse(packetStrings[1].Trim());
        if (Packets.CompareLists(p0, p1) <= 0)
        {
            sum += ix;
        }
        ix++;
    }
    Console.WriteLine(sum);