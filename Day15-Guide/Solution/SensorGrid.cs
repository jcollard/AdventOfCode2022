public record SensorGrid(List<Sensor> Sensors)
{
    // Given a Y coordinate to scan, returns a sorted list of Ranges containing
    // the X coordinate of positions that this SensorGrid can scan.
    public List<Range> DistinctRangesAtY(int y)
    {
        List<Range> ranges = new ();
        foreach (Sensor sensor in Sensors)
        {
            if (sensor.HasRangeAtY(y))
            {
                Range r = sensor.RangeAtY(y);
                ranges.Add(r);
            }
        }
        
        ranges = Range.MergeAll(ranges);
        return ranges;        
    }

    // Given a Y coordinate, returns the number of X coordinates that are
    // occupied by a beacon or a sensor.
    public int OccupiedSpacesAtY(int y)
    {
        HashSet<Position> positions = new ();
        foreach (Sensor sensor in Sensors)
        {
            if (sensor.Beacon.Y == y)
            {
                positions.Add(sensor.Beacon);
            }
            if (sensor.Y == y)
            {
                positions.Add(new Position(sensor.X, sensor.Y));
            }
        }
        return positions.Count;
    }

    // Given an array of strings representing the puzzle input parses a
    // SensorGrid
    public static SensorGrid Parse(string[] rows)
    {
        List<Sensor> sensors = new ();
        foreach (string row in rows)
        {
            sensors.Add(Sensor.Parse(row));
        }
        return new SensorGrid(sensors);
    }

}