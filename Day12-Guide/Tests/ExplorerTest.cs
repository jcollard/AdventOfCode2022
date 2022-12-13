namespace Tests;

public class ExplorerTest
{
    [Fact(Timeout = 5000)]
    public void TestExplore()
    {
        int[,] heights = {
            {2, 1, 2, 4},
            {3, 0, 3, 5},
            {9, 2, 3, 4},
        };

        Terrain terrain = new Terrain(heights);
        Position start = new Position(1,1);
        Explorer explorer = new Explorer(terrain, start);
        Assert.Contains(start, explorer.ExplorationQueue);
        Assert.Equal(1, explorer.ExplorationQueue.Count);
        Assert.True(explorer.IsExploring());
        // Queue: (1, 1)

        int[,] distances = {   
            {-1,  1, -1, -1},  // 2  1* 2 4
            {-1,  0, -1, -1},  // 3 [0] 3 5
            {-1, -1, -1, -1},  // 9  2* 3 4
        };
        Position result = explorer.Explore();
        Position expected = start;
        Assert.Equal(expected, result);
        Assert.Equal(distances, explorer.Distances);
        Assert.Contains(new Position(0, 1), explorer.ExplorationQueue);
        Assert.Equal(1, explorer.ExplorationQueue.Count);
        Assert.True(explorer.IsExploring());
        // Queue: (0, 1)

        distances = new int[,]{   
            { 2,  1,  2, -1},  // 2* [1] 2* 4
            {-1,  0, -1, -1},  // 3   0  3  5
            {-1, -1, -1, -1},  // 9   2  3  4
        };
        result = explorer.Explore();
        expected = new Position(0, 1);
        Assert.Equal(expected, result);
        Assert.Equal(distances, explorer.Distances);
        Assert.Contains(new Position(0, 2), explorer.ExplorationQueue);
        Assert.Contains(new Position(0, 0), explorer.ExplorationQueue);
        Assert.Equal(2, explorer.ExplorationQueue.Count);
        Assert.True(explorer.IsExploring());
        // Queue: (0, 2), (0, 0)

        distances = new int[,]{   
            { 2,  1,  2, -1},  // 2  1 [2]  4
            {-1,  0,  3, -1},  // 3  0  3*  5
            {-1, -1, -1, -1},  // 9  2  3   4
        };
        result = explorer.Explore();
        expected = new Position(0, 2);
        Assert.Equal(expected, result);
        Assert.Equal(distances, explorer.Distances);
        Assert.Contains(new Position(0, 0), explorer.ExplorationQueue);
        Assert.Contains(new Position(1, 2), explorer.ExplorationQueue);
        Assert.Equal(2, explorer.ExplorationQueue.Count);
        Assert.True(explorer.IsExploring());
        // Queue: (0, 0), (1, 2)

        distances = new int[,]{   
            { 2,  1,  2, -1},  // [2] 1  2   4
            { 3,  0,  3, -1},  //  3* 0  3   5
            {-1, -1, -1, -1},  //  9  2  3   4
        };
        result = explorer.Explore();
        expected = new Position(0, 0);
        Assert.Equal(expected, result);
        Assert.Equal(distances, explorer.Distances);
        Assert.Contains(new Position(1, 2), explorer.ExplorationQueue);
        Assert.Contains(new Position(1, 0), explorer.ExplorationQueue);
        Assert.Equal(2, explorer.ExplorationQueue.Count);
        Assert.True(explorer.IsExploring());
        // Queue: (1, 2), (1, 0)

        distances = new int[,]{   
            { 2,  1,  2, -1},  //  2  1   2   4
            { 3,  0,  3, -1},  //  3  0  [3]  5
            {-1, -1,  4, -1},  //  9  2   3*  4
        };
        result = explorer.Explore();
        expected = new Position(1, 2);
        Assert.Equal(expected, result);
        Assert.Equal(distances, explorer.Distances);
        Assert.Contains(new Position(1, 0), explorer.ExplorationQueue);
        Assert.Contains(new Position(2, 2), explorer.ExplorationQueue);
        Assert.Equal(2, explorer.ExplorationQueue.Count);
        Assert.True(explorer.IsExploring());
        // Queue: (1, 0), (2, 2)

        distances = new int[,]{   
            { 2,  1,  2, -1},  //  2  1   2   4
            { 3,  0,  3, -1},  // [3] 0   3   5
            {-1, -1,  4, -1},  //  9  2   3   4
        };
        result = explorer.Explore();
        expected = new Position(1, 0);
        Assert.Equal(expected, result);
        Assert.Equal(distances, explorer.Distances);
        Assert.Contains(new Position(2, 2), explorer.ExplorationQueue);
        Assert.Equal(1, explorer.ExplorationQueue.Count);
        Assert.True(explorer.IsExploring());
        // Queue: (2, 2)

        distances = new int[,]{   
            { 2,  1,  2, -1},  //  2  1   2   4
            { 3,  0,  3, -1},  //  3  0   3   5
            {-1,  5,  4,  5},  //  9  2* [3]  4*
        };
        result = explorer.Explore();
        expected = new Position(2, 2);
        Assert.Equal(expected, result);
        Assert.Equal(distances, explorer.Distances);
        Assert.Contains(new Position(2, 3), explorer.ExplorationQueue);
        Assert.Contains(new Position(2, 1), explorer.ExplorationQueue);
        Assert.Equal(2, explorer.ExplorationQueue.Count);
        Assert.True(explorer.IsExploring());
        // Queue: (2, 3), (2, 1)

        distances = new int[,]{   
            { 2,  1,  2, -1},  //  2  1   2   4
            { 3,  0,  3,  6},  //  3  0   3   5*
            {-1,  5,  4,  5},  //  9  2   3  [4]
        };
        result = explorer.Explore();
        expected = new Position(2, 3);
        Assert.Equal(expected, result);
        Assert.Equal(distances, explorer.Distances);
        Assert.Contains(new Position(2, 1), explorer.ExplorationQueue);
        Assert.Contains(new Position(1, 3), explorer.ExplorationQueue);
        Assert.Equal(2, explorer.ExplorationQueue.Count);
        Assert.True(explorer.IsExploring());
        // Queue: (2, 1), (1, 3)

        distances = new int[,]{   
            { 2,  1,  2, -1},  //  2  1   2   4
            { 3,  0,  3,  6},  //  3  0   3   5
            {-1,  5,  4,  5},  //  9 [2]  3   4
        };
        result = explorer.Explore();
        expected = new Position(2, 1);
        Assert.Equal(expected, result);
        Assert.Equal(distances, explorer.Distances);
        Assert.Contains(new Position(1, 3), explorer.ExplorationQueue);
        Assert.Equal(1, explorer.ExplorationQueue.Count);
        Assert.True(explorer.IsExploring());
        // Queue: (1, 3)

        distances = new int[,]{   
            { 2,  1,  2,  7},  //  2  1  2  4*
            { 3,  0,  3,  6},  //  3  0  3 [5]
            {-1,  5,  4,  5},  //  9  2  3  4
        };
        result = explorer.Explore();
        expected = new Position(1, 3);
        Assert.Equal(expected, result);
        Assert.Equal(distances, explorer.Distances);
        Assert.Contains(new Position(0, 3), explorer.ExplorationQueue);
        Assert.Equal(1, explorer.ExplorationQueue.Count);
        Assert.True(explorer.IsExploring());
        // Queue: (0, 3)

        distances = new int[,]{   
            { 2,  1,  2,  7},  //  2  1  2 [4]
            { 3,  0,  3,  6},  //  3  0  3  5
            {-1,  5,  4,  5},  //  9  2  3  4
        };
        result = explorer.Explore();
        expected = new Position(0, 3);
        Assert.Equal(expected, result);
        Assert.Equal(distances, explorer.Distances);
        Assert.Empty(explorer.ExplorationQueue);
        Assert.False(explorer.IsExploring());
        // Queue: {}
    }
}