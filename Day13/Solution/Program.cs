// See https://aka.ms/new-console-template for more information
string data = File.ReadAllText(args[0]);

Part2();

void Part2()
{
    List<Packet> packets = data
        .Split("\n")
        .Where(s => s.Trim() != string.Empty)
        .Select(s => s.Trim())
        .Select(Packet.Parse).ToList();

    Packet marker0 = Packet.Parse("[[2]]");
    Packet marker1 = Packet.Parse("[[6]]");
    packets.Add(marker0);
    packets.Add(marker1);
    packets.Sort((p0, p1) => p0.Compare(p1));
    int ix0 = packets.IndexOf(marker0) + 1;
    int ix1 = packets.IndexOf(marker1) + 1;
    Console.WriteLine($"{marker0} found at {ix0} and {marker1} found at {ix1}");
    Console.WriteLine($"Decoder key is {ix0 * ix1}");
}

void Part1()
{
    string[] pairs = data.Split("\n\n");
    int ix = 1;
    int sum = 0;
    foreach (string pair in pairs)
    {
        string[] packetStrings = pair.Split("\n");
        Packet p0 = Packet.Parse(packetStrings[0].Trim());
        Packet p1 = Packet.Parse(packetStrings[1].Trim());
        // Console.WriteLine($"{p0} vs {p1}");
        if (p0.Compare(p1) <= 0)
        {
            sum += ix;
        }
        ix++;
    }
    Console.WriteLine(sum);
}