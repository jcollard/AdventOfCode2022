public abstract record FileRef(string Path)
{
    public abstract int Size { get; }

}