string input = File.ReadAllText("input.txt");
FindWindow(input, 14);
int FindWindow(string toScan, int windowSize)
{

    for (int i = 0; i < toScan.Length - windowSize; i++)
    {
        string windowed = toScan[i..(i+windowSize)];
        if (windowed.ToHashSet().Count() == windowSize)
        {
            Console.WriteLine($"{windowed} @ {i} -> {i+windowSize}");
            return (i + windowSize);
        }
    }
    throw new Exception("Something terrible happened!");
}