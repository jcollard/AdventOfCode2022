namespace Tests;

public class HeightMapTest
{

    [Fact(Timeout = 5000)]
    public void TestHeightCheck()
    {
        Assert.True(HeightMap.CheckHeights(0, 0)); // Can step to
        Assert.True(HeightMap.CheckHeights(0, 1)); // Can step up
        Assert.False(HeightMap.CheckHeights(0, 2)); // Can't jump up.
        Assert.True(HeightMap.CheckHeights(2, 0)); // Can jump down
    }

    [Fact(Timeout = 5000)]
    public void TestFindNeighborCorner()
    {
        int[,] heights = {
            {0, 0, 0},
            {0, 0, 0},
            {0, 0, 0}
        };
        HeightMap map = new HeightMap(heights, new Position(0, 0, null), new Position(0, 0, null));
        
        Position topLeft = new Position(0, 0, null);
        HashSet<Position> expected = new (){ new Position(1, 0, topLeft), new Position(0, 1, topLeft) };
        Assert.Equal(expected, map.FindNeighbors(topLeft));

        Position topRight = new Position(0, 2, null);
        expected = new (){ new Position(0, 1, topRight), new Position(1, 2, topRight) };
        Assert.Equal(expected, map.FindNeighbors(topRight));

        Position bottomLeft  = new Position(2, 0, null);
        expected = new (){ new Position(2, 1, bottomLeft), new Position(1, 0, bottomLeft) };
        Assert.Equal(expected, map.FindNeighbors(bottomLeft));

        Position bottomRight  = new Position(2, 2, null);
        expected = new (){ new Position(1, 2, bottomRight), new Position(2, 1, bottomRight) };
        Assert.Equal(expected, map.FindNeighbors(bottomRight));
    }

    [Fact(Timeout = 5000)]
    public void TestFindNeighborLocalMinimum()
    {
        int[,] heights = {
            {2, 2, 2},
            {2, 0, 2},
            {2, 2, 2}
        };
        HeightMap map = new HeightMap(heights, new Position(0, 0, null), new Position(0, 0, null));
        Assert.Empty(map.FindNeighbors(new Position(1, 1, null)));
    }

    [Fact(Timeout = 5000)]
    public void TestFindNeighborLocalMaximum()
    {
        int[,] heights = {
            {2, 2, 2},
            {2, 4, 2},
            {2, 2, 2}
        };
        HeightMap map = new HeightMap(heights, new Position(0, 0, null), new Position(0, 0, null));
        Position start = new Position(1, 1, null);;
        HashSet<Position> expected = new ()
        {
            start.North, start.South, start.East, start.West
        };
        Assert.Equal(expected, map.FindNeighbors(start));
    }

    [Fact(Timeout = 5000)]
    public void TestFindNeighborSameHeight()
    {
        int[,] heights = {
            {2, 2, 2},
            {2, 2, 2},
            {2, 2, 2}
        };
        HeightMap map = new HeightMap(heights, new Position(0, 0, null), new Position(0, 0, null));
        Position start = new Position(1, 1, null);;
        HashSet<Position> expected = new ()
        {
            start.North, start.South, start.East, start.West
        };
        Assert.Equal(expected, map.FindNeighbors(start));
    }

    [Fact(Timeout = 5000)]
    public void TestFindNeighborNorthBlocked()
    {
        int[,] heights = {
            {2, 4, 2},
            {2, 2, 2},
            {2, 2, 2}
        };
        HeightMap map = new HeightMap(heights, new Position(0, 0, null), new Position(0, 0, null));
        Position start = new Position(1, 1, null);;
        HashSet<Position> expected = new ()
        {
            start.South, start.East, start.West
        };
        Assert.Equal(expected, map.FindNeighbors(start));
    }

    [Fact(Timeout = 5000)]
    public void TestFindNeighborEastBlocked()
    {
        int[,] heights = {
            {2, 2, 2},
            {2, 2, 4},
            {2, 2, 2}
        };
        HeightMap map = new HeightMap(heights, new Position(0, 0, null), new Position(0, 0, null));
        Position start = new Position(1, 1, null);;
        HashSet<Position> expected = new ()
        {
            start.South, start.North, start.West
        };
        Assert.Equal(expected, map.FindNeighbors(start));
    }

    [Fact(Timeout = 5000)]
    public void TestFindNeighborWestBlocked()
    {
        int[,] heights = {
            {2, 2, 2},
            {4, 2, 2},
            {2, 2, 2}
        };
        HeightMap map = new HeightMap(heights, new Position(0, 0, null), new Position(0, 0, null));
        Position start = new Position(1, 1, null);;
        HashSet<Position> expected = new ()
        {
            start.South, start.North, start.East
        };
        Assert.Equal(expected, map.FindNeighbors(start));
    }

    [Fact(Timeout = 5000)]
    public void TestFindNeighborSouthBlocked()
    {
        int[,] heights = {
            {2, 2, 2},
            {2, 2, 2},
            {2, 4, 2}
        };
        HeightMap map = new HeightMap(heights, new Position(0, 0, null), new Position(0, 0, null));
        Position start = new Position(1, 1, null);;
        HashSet<Position> expected = new ()
        {
            start.West, start.North, start.East
        };
        Assert.Equal(expected, map.FindNeighbors(start));
    }
}