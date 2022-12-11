public record MeanMonkey(Monkey ToConvert, int LCM) : Monkey(ToConvert.StartItems, ToConvert.Op, ToConvert.Divisor, ToConvert.TrueIx, ToConvert.FalseIx)
{

    protected override long NewVal(long oldVal)
    {
        return Op.Invoke(oldVal) % LCM;
    }

}