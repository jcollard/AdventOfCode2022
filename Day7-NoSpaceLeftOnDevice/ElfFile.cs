public record ElfFile : FileRef
{
    public override int Size { get; }
    public ElfFile(string path, int size) : base(path)
    {
        this.Size = size;
    }
}