string[] rows = File.ReadAllLines("input.txt");
Part2();
// 012345678901234567890
// ####
//   #
//    ###########
//            ###
//                ######
//                ###
void Part2()
{
    int maxRange = 4_000_000;
    Sensor[] sensors = rows.Select(Sensor.Parse).ToArray();
    // N is number of sensors
    // N ranges
    // O(N * LogN) // Sort ranges
    // 4m * (N * LogN)
    for (int y = 0; y <= maxRange; y++)
    {
        Range[] ranges = Ranges(sensors, y)
            .Select(r => r with { 
                Lower = Math.Clamp(r.Lower, 0, maxRange),
                Upper = Math.Clamp(r.Upper, 0, maxRange)
                })
            .ToArray();
        // Console.WriteLine($"Y = {y}");
        // Console.Write($"Ranges: ");
        // Console.WriteLine(string.Join(",\n", ranges.ToList()));
        if (ranges.Length == 0) continue;
        List<Range> disjoint = DisjointRanges(ranges);
        // Console.WriteLine($"Disjoint Sets: ");
        // Console.WriteLine(string.Join(",\n", disjoint));
        if (disjoint.Count > 1)
        {
            Console.WriteLine(string.Join(", ", disjoint));
            int x = disjoint[0].Upper + 1;
            Console.WriteLine($"The distress signal is at: X: {x}, Y: {y}");
            long freq = x * 4_000_000L + y;
            Console.WriteLine($"Frequency is {freq}");
            break;
        }
        // Console.ReadLine();
    }
    Console.WriteLine("I finished!");

}

Range[] Ranges(Sensor[] sensors, int y) => sensors
            .Where(s => s.HasRangeAtRow(y))
            .Select(s => s.RangeAtRow(y))
            .OrderBy(s => s.Lower)
            .ToArray();

List<Range> DisjointRanges(Range[] ranges)
{
    Range current = ranges[0];
    List<Range> disjointRanges = new () {  };

    foreach (Range r in ranges)
    {
        if (!current.Intersects(r))
        {
            disjointRanges.Add(current);
            current = r;
        }
        else
        {
            current = current.Merge(r);
        }
    }
    disjointRanges.Add(current);
    return disjointRanges;
}

void Part1()
{
    // int y = 2_000_000;
    int y = 10;
    Sensor[] sensors = rows.Select(Sensor.Parse).ToArray();
    Beacon[] beacons = sensors
        .Select(s => s.Beacon)
        .Where(b => b.Row == y)
        .ToHashSet().ToArray();
    Range[] ranges = sensors
        .Where(s => s.HasRangeAtRow(y))
        .Select(s => s.RangeAtRow(y))
        .OrderBy(s => s.Lower)
        .ToArray();

    Range current = ranges[0];
    List<Range> disjointRanges = new () {  };

    foreach (Range r in ranges)
    {
        if (!current.Intersects(r))
        {
            disjointRanges.Add(current);
            current = r;
        }
        else
        {
            current = current.Merge(r);
        }
    }
    Console.WriteLine(current);
    Console.WriteLine(string.Join(",", beacons.Select(b => b.ToString())));
    int beaconCount = beacons.Select(b => current.Contains(b.Col)).Count();
    Console.WriteLine($"Distress signal is not in {current.Elements - beaconCount} positions.");
    Console.WriteLine($"There were {disjointRanges.Count} disjoint ranges.");
}