// See https://aka.ms/new-console-template for more information
string data = File.ReadAllText("input.txt");
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