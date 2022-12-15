public record Sensor(int Row, int Col, Beacon Beacon)
{
    public int Radius { get; } = Math.Abs(Col - Beacon.Col) + Math.Abs(Row - Beacon.Row);
    public Range RangeAtRow(int row)
    {
        if (!HasRangeAtRow(row))
        {
            throw new Exception("Could not get Range.");
        }

        int rowDiff = Math.Abs(this.Row - row);
        int newRadius = Radius - rowDiff;
        return new Range(this.Col - newRadius, this.Col + newRadius);
    }

    public bool HasRangeAtRow(int row)
    {
        int rowDiff = Math.Abs(this.Row - row);
        int newRadius = Radius - rowDiff;
        return newRadius >= 0;
    }

    public static Sensor Parse(string row)
    {
        // Sensor at x=2, y=18: closest beacon is at x=-2, y=15
        int[] nums = row.Split(new[] { '=', ',', ':' }).Where(s => int.TryParse(s, out int _)).Select(int.Parse).ToArray();
        int sensorX = nums[0];
        int sensorY = nums[1];
        int beaconX = nums[2];
        int beaconY = nums[3];
        Beacon beacon = new Beacon(beaconY, beaconX);
        return new Sensor(sensorY, sensorX, beacon);
    }
}

public record Beacon(int Row, int Col);

public record Range(int Lower, int Upper)
{
    public int Elements => Upper - Lower + 1;

    public bool Contains(int x) => x >= this.Lower && x <= this.Upper;

    public Range Merge(Range other)
    {
        if (!this.Intersects(other)) throw new Exception("Cannot merge disjoint ranges.");
        int min = Math.Min(this.Lower, other.Lower);
        int max = Math.Max(this.Upper, other.Upper);
        return new Range(min, max);
    }
}

public static class RangeExtensions
{
    public static bool Contains(this Range r0, Range r1) => r0.Lower <= r1.Lower && r0.Upper >= r1.Upper;
    public static bool Contains(this Range r0, int value) => value >= r0.Lower && value <= r0.Upper;
    public static bool Intersects(this Range r0, Range r1) => r0.Contains(r1.Lower) || r1.Contains(r0.Lower);
    public static bool IsOverlapping(this Range r0, Range r1) => r0.Intersects(r1);
}