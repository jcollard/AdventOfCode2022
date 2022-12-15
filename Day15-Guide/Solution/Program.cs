int y = 2_000_000; // Target y

string[] rows = File.ReadAllLines("input.txt");
// Parse in the Grid
SensorGrid grid = SensorGrid.Parse(rows);
// Get a list of distinct ranges in the target row
List<Range> ranges = grid.DistinctRangesAtY(y);
// Count the elements in the distinct ranges
int elements = 0;
foreach (Range range in ranges)
{
    elements += range.Elements;
}
// Subtract out the spaces that are occupied
elements -= grid.OccupiedSpacesAtY(y);
Console.WriteLine($"Found {elements} positions that cannot contain the beacon.");