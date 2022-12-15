public record Range(int Lower, int Upper)
{
    public int Elements => Upper - Lower + 1;

    public bool Contains(int x) 
    {
        return x >= this.Lower && x <= this.Upper;
    }

    public bool Intersects(Range other)
    {
        return this.Contains(other.Lower) || other.Contains(this.Lower);
    }

    public static bool IsContinuous(Range left, Range right)
    {
        if (right.Lower < left.Lower)
        {
            return IsContinuous(right, left);
        }
        return left.Contains(right.Lower - 1) || left.Intersects(right); 
    }

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
