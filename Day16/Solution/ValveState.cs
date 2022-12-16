public record struct ValveState(long BitMask)
{
    public ValveState TurnOn(Location l)
    {
        return new ValveState(BitMask | (1L << l.Id));
    }

    public bool IsOn(Location l)
    {
        return (BitMask & (1L << l.Id)) > 0;
    }
}