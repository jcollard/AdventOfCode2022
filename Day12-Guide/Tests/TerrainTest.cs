namespace Tests;

public class HeightMapTest
{
    

    [Fact]
    public void FindNeighborsCorners()
    {
        int[,] heights = {
            {0, 0, 0},
            {0, 0, 0},
            {0, 0, 0},
        };
        Terrain map = new Terrain(heights);

        Position topLeft = new Position(0, 0);
        List<Position> neighbors = map.FindNeighbors(topLeft);
        List<Position> expected = new () { topLeft.East, topLeft.South };
        Assert.Equal(expected, neighbors);

        Position topRight = new Position(0, 2);
        neighbors = map.FindNeighbors(topRight);
        expected = new () { topRight.South, topRight.West };
        Assert.Equal(expected, neighbors);

        Position bottomLeft = new Position(2, 0);
        neighbors = map.FindNeighbors(bottomLeft);
        expected = new () { bottomLeft.North, bottomLeft.East };
        Assert.Equal(expected, neighbors);

        Position bottomRight = new Position(2, 2);
        neighbors = map.FindNeighbors(bottomRight);
        expected = new () { bottomRight.North, bottomRight.West };
        Assert.Equal(expected, neighbors);

        Position center = new Position(1, 1);
        neighbors = map.FindNeighbors(center);
        expected = new () { center.North, center.East, center.South, center.West };
        Assert.Equal(expected, neighbors);
    }

    [Fact]
    public void FindNeighborsAllDirs()
    {
        int[,] heights = {
            {0, 1, 0},
            {0, 0, 1},
            {0, 0, 0},
        };
        Terrain map = new Terrain(heights);
        Position center = new Position(1, 1);
        List<Position> result = map.FindNeighbors(center);
        List<Position> expected = new () { center.North, center.East, center.South, center.West };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindNeighborsNoDirs()
    {
        int[,] heights = {
            {0, 2, 0},
            {2, 0, 2},
            {0, 2, 0},
        };
        Terrain map = new Terrain(heights);
        Position center = new Position(1, 1);
        List<Position> result = map.FindNeighbors(center);
        Assert.Empty(result);
    }

    [Fact]
    public void FindNeighborsDifferentHeights()
    {
        int[,] heights = {
            {0, 9, 0},
            {3, 1, 2},
            {0, 0, 0},
        };
        Terrain map = new Terrain(heights);
        Position center = new Position(1, 1);
        List<Position> result = map.FindNeighbors(center);
        List<Position> expected = new () { center.East, center.South };
        Assert.Equal(expected, result);
    }

}