namespace Tests;
public class HeightMapTest
{
    public static readonly string[] EXAMPLE = {
        "30373",
        "25512",
        "65332",
        "33549",
        "35390"
    };

    [Fact(Timeout = 1000)]
    public void TestParseHeightMap()
    {
        int[,] heightMap = HeightMap.ParseHeightMap(EXAMPLE);
        int[,] expected = {
            {3, 0, 3, 7, 3},
            {2, 5, 5, 1, 2},
            {6, 5, 3, 3, 2},
            {3, 3, 5, 4, 9},
            {3, 5, 3, 9, 0}
        };
        Assert.Equal(expected, heightMap);
    }

    [Fact(Timeout = 1000)]
    public void TestIsVisibleEast()
    {
        int[,] heightMap = HeightMap.ParseHeightMap(EXAMPLE);
        Assert.False(HeightMap.IsVisibleEast(heightMap, 0, 0));
        Assert.False(HeightMap.IsVisibleEast(heightMap, 0, 1));
        Assert.False(HeightMap.IsVisibleEast(heightMap, 0, 2));
        Assert.True(HeightMap.IsVisibleEast(heightMap, 0, 3));
        Assert.True(HeightMap.IsVisibleEast(heightMap, 0, 4));

        Assert.False(HeightMap.IsVisibleEast(heightMap, 1, 0));
        Assert.False(HeightMap.IsVisibleEast(heightMap, 1, 1));
        Assert.True(HeightMap.IsVisibleEast(heightMap, 1, 2));
        Assert.False(HeightMap.IsVisibleEast(heightMap, 1, 3));
        Assert.True(HeightMap.IsVisibleEast(heightMap, 1, 4));

        Assert.True(HeightMap.IsVisibleEast(heightMap, 2, 0));
        Assert.True(HeightMap.IsVisibleEast(heightMap, 2, 1));
        Assert.False(HeightMap.IsVisibleEast(heightMap, 2, 2));
        Assert.True(HeightMap.IsVisibleEast(heightMap, 2, 3));
        Assert.True(HeightMap.IsVisibleEast(heightMap, 2, 4));

        Assert.False(HeightMap.IsVisibleEast(heightMap, 3, 0));
        Assert.False(HeightMap.IsVisibleEast(heightMap, 3, 1));
        Assert.False(HeightMap.IsVisibleEast(heightMap, 3, 2));
        Assert.False(HeightMap.IsVisibleEast(heightMap, 3, 3));
        Assert.True(HeightMap.IsVisibleEast(heightMap, 3, 4));

        Assert.False(HeightMap.IsVisibleEast(heightMap, 4, 0));
        Assert.False(HeightMap.IsVisibleEast(heightMap, 4, 1));
        Assert.False(HeightMap.IsVisibleEast(heightMap, 4, 2));
        Assert.True(HeightMap.IsVisibleEast(heightMap, 4, 3));
        Assert.True(HeightMap.IsVisibleEast(heightMap, 4, 4));
    }

    [Fact(Timeout = 1000)]
    public void TestIsVisibleWest()
    {
        int[,] heightMap = HeightMap.ParseHeightMap(EXAMPLE);
        Assert.True(HeightMap.IsVisibleWest(heightMap, 0, 0));
        Assert.False(HeightMap.IsVisibleWest(heightMap, 0, 1));
        Assert.False(HeightMap.IsVisibleWest(heightMap, 0, 2));
        Assert.True(HeightMap.IsVisibleWest(heightMap, 0, 3));
        Assert.False(HeightMap.IsVisibleWest(heightMap, 0, 4));

        Assert.True(HeightMap.IsVisibleWest(heightMap, 1, 0));
        Assert.True(HeightMap.IsVisibleWest(heightMap, 1, 1));
        Assert.False(HeightMap.IsVisibleWest(heightMap, 1, 2));
        Assert.False(HeightMap.IsVisibleWest(heightMap, 1, 3));
        Assert.False(HeightMap.IsVisibleWest(heightMap, 1, 4));

        Assert.True(HeightMap.IsVisibleWest(heightMap, 2, 0));
        Assert.False(HeightMap.IsVisibleWest(heightMap, 2, 1));
        Assert.False(HeightMap.IsVisibleWest(heightMap, 2, 2));
        Assert.False(HeightMap.IsVisibleWest(heightMap, 2, 3));
        Assert.False(HeightMap.IsVisibleWest(heightMap, 2, 4));

        Assert.True(HeightMap.IsVisibleWest(heightMap, 3, 0));
        Assert.False(HeightMap.IsVisibleWest(heightMap, 3, 1));
        Assert.True(HeightMap.IsVisibleWest(heightMap, 3, 2));
        Assert.False(HeightMap.IsVisibleWest(heightMap, 3, 3));
        Assert.True(HeightMap.IsVisibleWest(heightMap, 3, 4));

        Assert.True(HeightMap.IsVisibleWest(heightMap, 4, 0));
        Assert.True(HeightMap.IsVisibleWest(heightMap, 4, 1));
        Assert.False(HeightMap.IsVisibleWest(heightMap, 4, 2));
        Assert.True(HeightMap.IsVisibleWest(heightMap, 4, 3));
        Assert.False(HeightMap.IsVisibleWest(heightMap, 4, 4));
    }

    [Fact(Timeout = 1000)]
    public void TestIsVisibleNorth()
    {
        int[,] heightMap = HeightMap.ParseHeightMap(EXAMPLE);
        Assert.True(HeightMap.IsVisibleNorth(heightMap, 0, 0));
        Assert.True(HeightMap.IsVisibleNorth(heightMap, 0, 1));
        Assert.True(HeightMap.IsVisibleNorth(heightMap, 0, 2));
        Assert.True(HeightMap.IsVisibleNorth(heightMap, 0, 3));
        Assert.True(HeightMap.IsVisibleNorth(heightMap, 0, 4));

        Assert.False(HeightMap.IsVisibleNorth(heightMap, 1, 0));
        Assert.True(HeightMap.IsVisibleNorth(heightMap, 1, 1));
        Assert.True(HeightMap.IsVisibleNorth(heightMap, 1, 2));
        Assert.False(HeightMap.IsVisibleNorth(heightMap, 1, 3));
        Assert.False(HeightMap.IsVisibleNorth(heightMap, 1, 4));

        Assert.True(HeightMap.IsVisibleNorth(heightMap, 2, 0));
        Assert.False(HeightMap.IsVisibleNorth(heightMap, 2, 1));
        Assert.False(HeightMap.IsVisibleNorth(heightMap, 2, 2));
        Assert.False(HeightMap.IsVisibleNorth(heightMap, 2, 3));
        Assert.False(HeightMap.IsVisibleNorth(heightMap, 2, 4));

        Assert.False(HeightMap.IsVisibleNorth(heightMap, 3, 0));
        Assert.False(HeightMap.IsVisibleNorth(heightMap, 3, 1));
        Assert.False(HeightMap.IsVisibleNorth(heightMap, 3, 2));
        Assert.False(HeightMap.IsVisibleNorth(heightMap, 3, 3));
        Assert.True(HeightMap.IsVisibleNorth(heightMap, 3, 4));

        Assert.False(HeightMap.IsVisibleNorth(heightMap, 4, 0));
        Assert.False(HeightMap.IsVisibleNorth(heightMap, 4, 1));
        Assert.False(HeightMap.IsVisibleNorth(heightMap, 4, 2));
        Assert.True(HeightMap.IsVisibleNorth(heightMap, 4, 3));
        Assert.False(HeightMap.IsVisibleNorth(heightMap, 4, 4));
    }

    [Fact(Timeout = 1000)]
    public void TestIsVisibleSouth()
    {
        int[,] heightMap = HeightMap.ParseHeightMap(EXAMPLE);
        Assert.False(HeightMap.IsVisibleSouth(heightMap, 0, 0));
        Assert.False(HeightMap.IsVisibleSouth(heightMap, 0, 1));
        Assert.False(HeightMap.IsVisibleSouth(heightMap, 0, 2));
        Assert.False(HeightMap.IsVisibleSouth(heightMap, 0, 3));
        Assert.False(HeightMap.IsVisibleSouth(heightMap, 0, 4));

        Assert.False(HeightMap.IsVisibleSouth(heightMap, 1, 0));
        Assert.False(HeightMap.IsVisibleSouth(heightMap, 1, 1));
        Assert.False(HeightMap.IsVisibleSouth(heightMap, 1, 2));
        Assert.False(HeightMap.IsVisibleSouth(heightMap, 1, 3));
        Assert.False(HeightMap.IsVisibleSouth(heightMap, 1, 4));

        Assert.True(HeightMap.IsVisibleSouth(heightMap, 2, 0));
        Assert.False(HeightMap.IsVisibleSouth(heightMap, 2, 1));
        Assert.False(HeightMap.IsVisibleSouth(heightMap, 2, 2));
        Assert.False(HeightMap.IsVisibleSouth(heightMap, 2, 3));
        Assert.False(HeightMap.IsVisibleSouth(heightMap, 2, 4));

        Assert.False(HeightMap.IsVisibleSouth(heightMap, 3, 0));
        Assert.False(HeightMap.IsVisibleSouth(heightMap, 3, 1));
        Assert.True(HeightMap.IsVisibleSouth(heightMap, 3, 2));
        Assert.False(HeightMap.IsVisibleSouth(heightMap, 3, 3));
        Assert.True(HeightMap.IsVisibleSouth(heightMap, 3, 4));

        Assert.True(HeightMap.IsVisibleSouth(heightMap, 4, 0));
        Assert.True(HeightMap.IsVisibleSouth(heightMap, 4, 1));
        Assert.True(HeightMap.IsVisibleSouth(heightMap, 4, 2));
        Assert.True(HeightMap.IsVisibleSouth(heightMap, 4, 3));
        Assert.True(HeightMap.IsVisibleSouth(heightMap, 4, 4));
    }

    [Fact(Timeout = 1000)]
    public void TestIsVisible()
    {
        int[,] heightMap = HeightMap.ParseHeightMap(EXAMPLE);
        Assert.True(HeightMap.IsVisible(heightMap, 0, 0));
        Assert.True(HeightMap.IsVisible(heightMap, 0, 1));
        Assert.True(HeightMap.IsVisible(heightMap, 0, 2));
        Assert.True(HeightMap.IsVisible(heightMap, 0, 3));
        Assert.True(HeightMap.IsVisible(heightMap, 0, 4));

        Assert.True(HeightMap.IsVisible(heightMap, 1, 0));
        Assert.True(HeightMap.IsVisible(heightMap, 1, 1));
        Assert.True(HeightMap.IsVisible(heightMap, 1, 2));
        Assert.False(HeightMap.IsVisible(heightMap, 1, 3));
        Assert.True(HeightMap.IsVisible(heightMap, 1, 4));

        Assert.True(HeightMap.IsVisible(heightMap, 2, 0));
        Assert.True(HeightMap.IsVisible(heightMap, 2, 1));
        Assert.False(HeightMap.IsVisible(heightMap, 2, 2));
        Assert.True(HeightMap.IsVisible(heightMap, 2, 3));
        Assert.True(HeightMap.IsVisible(heightMap, 2, 4));

        Assert.True(HeightMap.IsVisible(heightMap, 3, 0));
        Assert.False(HeightMap.IsVisible(heightMap, 3, 1));
        Assert.True(HeightMap.IsVisible(heightMap, 3, 2));
        Assert.False(HeightMap.IsVisible(heightMap, 3, 3));
        Assert.True(HeightMap.IsVisible(heightMap, 3, 4));

        Assert.True(HeightMap.IsVisible(heightMap, 4, 0));
        Assert.True(HeightMap.IsVisible(heightMap, 4, 1));
        Assert.True(HeightMap.IsVisible(heightMap, 4, 2));
        Assert.True(HeightMap.IsVisible(heightMap, 4, 3));
        Assert.True(HeightMap.IsVisible(heightMap, 4, 4));
    }
}