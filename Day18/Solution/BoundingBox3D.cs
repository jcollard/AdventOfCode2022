public record BoundingBox3D(
    int MinX, int MaxX,
    int MinY, int MaxY,
    int MinZ, int MaxZ
)
{
    public bool Contains(Position p)
    {
        return p.X >= MinX && p.X <= MaxX &&
               p.Y >= MinY && p.Y <= MaxY &&
               p.Z >= MinZ && p.Z <= MaxZ;
    }

    public BoundingBox3D Pad(int amount)
    {
        return new BoundingBox3D(
            MinX - amount,
            MinY - amount,
            MinZ - amount,
            MaxX + amount,
            MaxY + amount,
            MaxZ + amount
        );
    }

    public static BoundingBox3D Find(HashSet<Position> shape)
    {
        int minX, minY, minZ;
        int maxX, maxY, maxZ;
        minX = minY = minZ = int.MaxValue;
        maxX = maxY = maxZ = int.MinValue;
        foreach(Position p in shape)
        {
            minX = Math.Min(minX, p.X);
            minY = Math.Min(minY, p.Y);
            minZ = Math.Min(minZ, p.Z);   
            maxX = Math.Max(maxX, p.X);
            maxY = Math.Max(maxY, p.Y);
            maxZ = Math.Max(maxZ, p.Z);   
        }
        return new BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
    }

}