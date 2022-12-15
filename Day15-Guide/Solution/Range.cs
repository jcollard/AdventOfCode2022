public record Range(int Lower, int Upper)
{
    public int Elements => Upper - Lower + 1;

    // Returns true if the specified value is contained in this Range
    public bool Contains(int x) 
    {
        return x >= this.Lower && x <= this.Upper;
    }

    // Returns true if the specified range intersects with this Range
    public bool Intersects(Range other)
    {
        return this.Contains(other.Lower) || other.Contains(this.Lower);
    }

    // Returns the range that is the result of merging two continous ranges
    // together
    public static bool IsContinuous(Range left, Range right)
    {
        if (right.Lower < left.Lower)
        {
            return IsContinuous(right, left);
        }
        return left.Contains(right.Lower - 1) || left.Intersects(right); 
    }

    // Given two ranges, returns true if the ranges are continuous. 
    public Range MergeWith(Range other)
    {
        if (!IsContinuous(this, other))
        {
            throw new Exception($"Could not merge range {this} with {other}.");
        }
        int min = Math.Min(this.Lower, other.Lower);
        int max = Math.Max(this.Upper, other.Upper);
        return new Range(min, max);
    }

    // Given a List of Ranges, sorts and merges them together. The returned list
    // contains distinct Ranges sorted from lowest to highest.
    public static List<Range> MergeAll(List<Range> toMerge)
    {
        toMerge.Sort((r0, r1) => r0.Lower - r1.Lower);
        List<Range> ranges = new ();
        Range current = toMerge[0];
        foreach (Range r in toMerge)
        {
            if (IsContinuous(current, r))
            {
                current = current.MergeWith(r);
            }
            else
            {
                ranges.Add(current);
                current = r;
            }
        }
        ranges.Add(current);
        return ranges;
    }
}
