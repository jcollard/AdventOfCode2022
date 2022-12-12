namespace Tests;

public class SolverTest
{
    private static readonly string INPUT = @"abcccccccccccccccccaaaaaaaaccccccacccaaccccccccccccccccccaaaaaaaaaacccccccccccccccccccccccccccccccaaaaaaccccccccccccccccccccccccccccccccccaaaaa
abccccccccccccccccccaaaaacccccccaaaaaaacccccccccccaaccaaaaaaaaaaaaaccccccccccccccccccccccccccccccccaaaaacccccccccccccccccccccccccccccccccaaaaaa
abccccccccccccaaccccaaaaaacccccccaaaaaaaaccccccacaaaccaaaaaaaaaaaaaaaccccccccccccccccccaaacccccccaaaaaaaccccccccccccccccaaaccccccccccccccaaaaaa
abccccccccacccaaccccaaaaaacccccccaaaaaaaaaccccaaaaaaaaacaaaaaaaaaaaaacccccccccccccccccccaacccccccaaaaaaaacccccccccccccccaaaccccccccccccccaccaaa
abaacccccaaaaaaaccccaaaccacccccccaaaaaaaaaccccaaaaaaaaccccaaaaaaaaaaaccccccccccccccccaacaaaaaccccaaaaaaaacccccccccccccccaaacccccccccccccccccaaa
abaaccccccaaaaaaaacccccccccccccccaaaaaaaaccccccaaaaaacccccaaaacaaaaccccccccccccccccccaaaaaaaaccccccaaacaccccccccccccccccaaakccaaaccccccccccccaa
abaaacccccaaaaaaaaaccccccccccccccaaaaaaacccccccaaaaaccccccaaaccaaaaccccccccccccaacacccaaaaaccccccccaaacccccccccccccacacckkkkkkkaacccccccccccccc
abaaacccccaaaaaaaaaccccccccccccccaccaaaaaccccccaaaaaacccccaaacaaaccccccccccccccaaaaccccaaaaacccccccccccccccccccccccaaaakkkkkkkkkacccaaaccaccccc
abacacccccaaaaaaaccccccccccccccccccccaaaaaaaccccccaaccccccaaaaaaaaccccccccccccaaaaacccaaacaacccccccccccccccccccccccaajkkkkppkkkkccccaaaaaaccccc
abacccccccaaaaaaacccccccccccccccccccaaaaaaaaccccccccccccccccaaaaaaccccccccccccaaaaaacccaacccccccccccccccccccccccccccjjkkooppppkllccccaaaaaccccc
abccccccccaccaaaccccccccccccccccccccaaaaaaaacccccccccccccccccaaaaaccccccccccccacaaaacccccccccccccccccccccccccccccjjjjjjoooppppklllcacaaaaaccccc
abcccaacccccccaaacccccccccccccccccccaaaaaaacccccccccccccccccaaaaacccccccccccccccaacaccccccccccccccccccccccccccjjjjjjjjoooopuppplllcccccaaaacccc
abcccaacccccccccccccccccaaacccccccccccaaaaaaccccccaaaaacccccaaaaaccccccccccccaaacaaacccccaaaccccccccccccccccijjjjjjjjooouuuuuppllllcccccaaacccc
abaaaaaaaaccccccccccccccaaaaccccccccccaacaaaccccccaaaaaccccccccccccccccccccccaaaaaaacccccaaacacccccccccccccciijjoooooooouuuuuppplllllccccaccccc
abaaaaaaaaccccccccccccccaaaaccccccccccaacccccccccaaaaaacccccccccccccccccccccccaaaaaacccaaaaaaaacccccccccccciiiqqooooooouuuxuuuppplllllccccccccc
abccaaaaccccccccccccccccaaaccccccccccccccccccccccaaaaaacccccccccccccccccccccccaaaaaaaccaaaaaaaacccccccccccciiiqqqqtttuuuuxxxuupppqqllllmccccccc
abcaaaaacccaaaccccccccccccccccccccccccaccccccccccaaaaaacccccccccccccccccccccaaaaaaaaaaccaaaaaaccccccccccccciiiqqqtttttuuuxxxuuvpqqqqmmmmccccccc
abcaacaaaccaaacaaccccccccccccccccccccaaaacaaaccccccaacccaaaaacccccccccccccccaaaaaaaaaacccaaaaacccaaaccccccciiiqqttttxxxxxxxyuvvvvqqqqmmmmcccccc
abcacccaaccaaaaaaccccccccccccccccccccaaaaaaaacccccccccccaaaaacccccccccccccccaaacaaacccccaaaaaaccaaaacccccaaiiiqqtttxxxxxxxxyyvvvvvvqqqmmmdddccc
abcccccccaaaaaaaccccccccccccccccccccccaaaaaaaaacccccccccaaaaaaccccccccccccccccccaaaccccccaacccccaaaacccaaaaiiiqqqttxxxxxxxyyyyyyvvvqqqmmmdddccc
SbccccccccaaaaaccccccccaacaaccccccccaaaaaaaaaaccccccccccaaaaaaccccccccccccaaacccaaccccccccccccccaaaacccaaaaaiiiqqtttxxxxEzzyyyyvvvvqqqmmmdddccc
abaccccccccaaaaacccccccaaaaacccccccaaaaaaaaaaaccccccccccaaaaaaccccccccccaaaaaacccccccccccccccccccccccccaaaaaiiiqqqtttxxxyyyyyyvvvvqqqmmmdddcccc
abaacccccccaacaaaccccccaaaaaacccccccaaaaaaaaaaccccccccccccaaacccccccccccaaaaaaccccccccccccccccccccccccccaaaahhhqqqqttxxyyyyyyvvvvqqqmmmddddcccc
abaccccccccaaccccccccccaaaaaacccaacaaccaaaaaaaaaccccccccccccccccccccccccaaaaaaccccccccccccccccccccccccccaaaachhhqqtttxwyyyyyywvrqqqmmmmdddccccc
abaaaccccccccccccccccccaaaaaacccaaaaaccaaaaacaaaccccccccccccccccccccccccaaaaaccccaaaaccccaaaccccccccccccccccchhhppttwwwywwyyywwrrrnmmmdddcccccc
abaaaccccccccccccccccccccaaaccccaaaaaacaaaaaaaaaccccccccaaacccccccccccccaaaaaccccaaaaccccaaaccccccccccccccccchhpppsswwwwwwwwywwrrrnnndddccccccc
abaaacccccccccccccccccccccccccccaaaaaacccaaaaaacccccccccaaaaacccccaacccccccccccccaaaacaaaaaaaaccccccccccccccchhpppsswwwwsswwwwwrrrnneeddccccccc
abaccccccccaaaacccccccccccccccccaaaaaaccccaaaaaaaacccccaaaaaaccaacaaacccccccccccccaaccaaaaaaaaccccccccccccccchhpppssssssssrwwwwrrrnneeecaaccccc
abaccccccccaaaacccccccccccccccccccaaaccccaaaaaaaaacccccaaaaaaccaaaaaccccccccccccccccccccaaaaacccccccccccccccchhpppssssssssrrrwrrrnnneeeaaaccccc
abcccccccccaaaacccccccccccccccccccccccccaaaaaaaaaaccccccaaaaacccaaaaaacccccccccccccccccaaaaaacccccccccccccccchhpppppsssooorrrrrrrnnneeeaaaccccc
abcccccccccaaaccccccccccccccccccccccccccaaacaaacccccccccaacaacaaaaaaaacccccccccccccccccaaaaaacaaccccccccccccchhhppppppoooooorrrrnnneeeaaaaacccc
abccccccccccccccccccccccccccccccccccccccccccaaaccaaaacccccccccaaaaacaaccccaacccccccccacaaaaaacaaccccccccccccchhhgpppppoooooooonnnnneeeaaaaacccc
abcccccccaacccccccccccccccccccccccccccccccccaaacaaaaaccccccccccacaaaccccccaacccccccccaacaaaaaaaaaaacccccaaccccgggggggggggfooooonnneeeeaaaaacccc
abcccccccaaacaaccccccccccccaacccccccccccccccccccaaaaaaccccaacccccaaacccaaaaaaaaccccccaaaaacaaaaaaaaccccaaacccccggggggggggfffooonneeeecaaacccccc
abcccccccaaaaaaccccaacccccaaacccccccccccccccccccaaaaaaccccaaaccccccccccaaaaaaaacccccccaaaaaccaaaaccccaaaaaaaacccggggggggfffffffffeeeecaaccccccc
abcccccaaaaaaaccaaaaacaaaaaaacccccccccccccccccccaaaaacccccaaaacccaaccccccaaaacccccccaaaaaaaacaaaaacccaaaaaaaaccccccccccaaaffffffffecccccccccccc
abcaaacaaaaaaacccaaaaaaaaaaaaaaaccccccccccccccccccaaacccccaaaacaaaacaacccaaaaaccccccaaaaaaaaaaaaaaccccaaaaaacccccccccccaaacaafffffccccccccccaaa
abaaaacccaaaaaaccaaaaacaaaaaaaaaccccccccccccaaacccccccccccaaaaaaaaacaaccaaacaacccccccccaacccaaccaaccccaaaaaaccccccccccaaaaccaaacccccccccccccaaa
abaaaacccaacaaacaaaaacccaaaaaaacccccccccccccaaaacccccccccaaaaaaaaaaaaaccaacccacccccccccaacccccccccccccaaaaaaccccccccccaaacccccccccccccccccccaaa
abcaaacccaacccccccaaaccaaaaaacccccccccccccccaaaaccccccaaaaaaaaaaaaaaaaaaccccccccccccccccccccccccccccccaaccaaccccccccccaaaccccccccccccccccaaaaaa
abcccccccccccccccccccccaaaaaaaccccccccccccccaaacccccccaaaaaaaaaaaaaaaaaacccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccaaaaaa".Trim();

    private static readonly string EXAMPLE = @"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi".Trim();

    [Fact(Timeout = 5000)]
    public void TestFindNeighborAllDirections()
    {

        string[] rows = {
            "Sabqponm",
            "abcryxxl",
            "accszExk",
            "acctuvwj",
            "abdefghi",
        };


        HeightMap map = HeightMap.Parse(rows);
        Solver solver = new Solver(map);

        Position toCheck = new Position(1, 1, null);
        HashSet<(int, int)> visited = new();
        HashSet<(int, int)> result = solver.FindNeighbors(toCheck, visited).Select(s => s.AsPair).ToHashSet();
        // HashSet<(int, int)> expected = new () { (0, 1), (1, 0), (1, 2), (2, 1) };
        Assert.Equal(4, result.Count);

        visited.Add(toCheck.North.AsPair);
        result = solver.FindNeighbors(toCheck, visited).Select(s => s.AsPair).ToHashSet();
        Assert.Equal(3, result.Count);

        visited.Add(toCheck.South.AsPair);
        result = solver.FindNeighbors(toCheck, visited).Select(s => s.AsPair).ToHashSet();
        Assert.Equal(2, result.Count);

        visited.Add(toCheck.East.AsPair);
        result = solver.FindNeighbors(toCheck, visited).Select(s => s.AsPair).ToHashSet();
        Assert.Single(result);

        visited.Add(toCheck.West.AsPair);
        result = solver.FindNeighbors(toCheck, visited).Select(s => s.AsPair).ToHashSet();
        Assert.Empty(result);
    }

    [Fact(Timeout = 5000)]
    public void TestNeighborWith2Height()
    {

        string[] rows = {
            "Sabqponm",
            "abcryxxl",
            "accszExk",
            "acctuvwj",
            "abdefghi",
        };


        HeightMap map = HeightMap.Parse(rows);
        Solver solver = new Solver(map);

        Position toCheck = new Position(2, 0, null);
        HashSet<(int, int)> visited = new();
        HashSet<(int, int)> result = solver.FindNeighbors(toCheck, visited).Select(s => s.AsPair).ToHashSet();
        Assert.Equal(2, result.Count);

        visited.Add((1, 0));
        result = solver.FindNeighbors(toCheck, visited).Select(s => s.AsPair).ToHashSet();
        Assert.Single(result);

        visited.Add((3, 0));
        result = solver.FindNeighbors(toCheck, visited).Select(s => s.AsPair).ToHashSet();
        Assert.Empty(result);
    }

    [Fact(Timeout = 5000)]
    public void TestNeighborJumpDown()
    {

        string[] rows = {
            "Sabqponm",
            "abcryxxl",
            "accszExk",
            "acctuvwj",
            "abdefghi",
        };


        HeightMap map = HeightMap.Parse(rows);
        Solver solver = new Solver(map);

        Position toCheck = new Position(2, 4, null);
        HashSet<(int, int)> visited = new();
        HashSet<(int, int)> result = solver.FindNeighbors(toCheck, visited).Select(s => s.AsPair).ToHashSet();
        Assert.Equal(4, result.Count);

        visited.Add(toCheck.North.AsPair);
        result = solver.FindNeighbors(toCheck, visited).Select(s => s.AsPair).ToHashSet();
        Assert.Equal(3, result.Count);

        visited.Add(toCheck.South.AsPair);
        result = solver.FindNeighbors(toCheck, visited).Select(s => s.AsPair).ToHashSet();
        Assert.Equal(2, result.Count);

        visited.Add(toCheck.East.AsPair);
        result = solver.FindNeighbors(toCheck, visited).Select(s => s.AsPair).ToHashSet();
        Assert.Single(result);

        visited.Add(toCheck.West.AsPair);
        result = solver.FindNeighbors(toCheck, visited).Select(s => s.AsPair).ToHashSet();
        Assert.Empty(result);
    }

    [Fact(Timeout = 5000)]
    public void TestCorners()
    {

        string[] rows = {
            "Sabqponm",
            "abcryxxl",
            "accszExk",
            "acctuvwj",
            "abdefghi",
        };


        HeightMap map = HeightMap.Parse(rows);
        Solver solver = new Solver(map);

        Position toCheck = new Position(0, 0, null);
        HashSet<(int, int)> visited = new();
        HashSet<(int, int)> result = solver.FindNeighbors(toCheck, visited).Select(s => s.AsPair).ToHashSet();
        Assert.Equal(2, result.Count);

        toCheck = new Position(4, 0, null);
        visited = new();
        result = solver.FindNeighbors(toCheck, visited).Select(s => s.AsPair).ToHashSet();
        Assert.Equal(2, result.Count);

        toCheck = new Position(4, 7, null);
        visited = new();
        result = solver.FindNeighbors(toCheck, visited).Select(s => s.AsPair).ToHashSet();
        Assert.Equal(2, result.Count);

        toCheck = new Position(0, 7, null);
        visited = new();
        result = solver.FindNeighbors(toCheck, visited).Select(s => s.AsPair).ToHashSet();
        Assert.Equal(2, result.Count);
    }
}