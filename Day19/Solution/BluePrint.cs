public record BluePrint(
    int ID,
    Cost OreRobot,
    Cost ClayRobot,
    Cost ObsidianRobot,
    Cost GeodeRobot
)
{
    public static BluePrint Parse(string row)
    {
        // 1 3 3 2 20 2 20
        int[] tokens = row
            .Replace("Blueprint ", "")
            .Replace(": Each ore robot costs ", " ")
            .Replace(" ore. Each clay robot costs ", " ")
            .Replace(" ore. Each obsidian robot costs ", " ")
            .Replace(" ore and ", " ")
            .Replace(" clay. Each geode robot costs ", " ")
            .Replace(" ore and ", " ")
            .Replace(" obsidian.", "")
            .Split(" ")
            .Select(int.Parse)
            .ToArray();
        int id = tokens[0];
        Cost oreBot = new Cost(tokens[1], 0, 0);    
        Cost clayBot = new Cost(tokens[2], 0, 0);
        Cost obsidianBot = new Cost(tokens[3], tokens[4], 0);
        Cost geodeBot = new Cost(tokens[5], 0, tokens[6]);
        return new BluePrint(id, oreBot, clayBot, obsidianBot, geodeBot);
    }
}

public record Cost(
    int Ore,
    int Clay,
    int Obsidian
);