public record ElfDirectory : FileRef
{
    private List<FileRef> _children { get; } = new ();
    public override int Size => _children.Select(c => c.Size).Sum();

    public ElfDirectory(string path) : base (path) { }

    public FileRef AddChild(FileRef child)
    {
        _children.Add(child);
        return this;
    }

    public List<FileRef>.Enumerator Children()
    {
        return _children.GetEnumerator();
    }
}