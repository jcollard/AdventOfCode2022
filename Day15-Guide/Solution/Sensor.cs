public record Sensor(int X, int Y, Position Beacon)
{
    public int Radius { get; } = Math.Abs(X - Beacon.X) + Math.Abs(Y - Beacon.Y);

    public bool HasRangeAtY(int y)
    {
        int yDiff = Math.Abs(Y - y);
        return Radius - yDiff >= 0;
    }

    public Range RangeAtY(int y)
    {
        if (!HasRangeAtY(y))
        {
            throw new Exception($"No range found at Y={y}");
        }
        int yDiff = Math.Abs(Y - y);
        int newRadius = Radius - yDiff;
        return new Range(X - newRadius, X + newRadius);
    }

    public static Sensor Parse(string row)
    {
        // Sensor at x=2, y=18: closest beacon is at x=-2, y=15
        string[] nums = row
            .Replace("Sensor at x=", "")
            .Replace(", y=", " ")
            .Replace(": closest beacon is at x=", " ")
            .Split();
        int sensorX = int.Parse(nums[0]);
        int sensorY = int.Parse(nums[1]);
        int beaconX = int.Parse(nums[2]);
        int beaconY = int.Parse(nums[3]);
        Position beacon = new (beaconX, beaconY);
        return new Sensor(sensorX, sensorY, beacon);
    }
}