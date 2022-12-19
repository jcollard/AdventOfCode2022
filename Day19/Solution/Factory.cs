public record Factory(BluePrint BluePrint, StockPile StockPile, Bots Bots)
{
    public const int INF = 99;

    public override string ToString()
    {
        string[] rows = {
            $"Factory: ",
            $"  Stock: Or ({StockPile.Ore, 2}) | Cl ({StockPile.Clay, 2}) | Ob ({StockPile.Obsidian, 2}) | Ge ({StockPile.Geode, 2})",
            $"  Bots : Or ({Bots.Ore, 2}) | Cl ({Bots.Clay, 2}) | Ob ({Bots.Obsidian, 2}) | Ge ({Bots.Geode, 2})",

        };
        return string.Join('\n', rows);
    }

    public List<(BotType, Factory)> Outcomes(int time, HashSet<BotType> optionsLastTime)
    {
        List<(BotType, Factory)> choices = new();

        // If you have the resources, you can choose to make a bot OR not
        // TODO: Start with just 1... but maybe not
        foreach (BotType t in ReasonableChoices(time, optionsLastTime))
        {
            choices.Add((t, CollectResourcesAndBuild(t, time)));
        }

        return choices;
    }

    public List<BotType> ReasonableChoices(int time, HashSet<BotType> optionsLastTime)
    {
        List<BotType> potentialBuilds = new() { BotType.None };
        
        // Something about the cost of the bots is important here
        // Essentially, in our inputs, it is SO expensive to produce
        // Geode/Obsidian bots, if you can do it, you should. But, there are easily
        // degenerate cases that prove this incorrect for low cost Geode / Obsidian bots.
        if (!optionsLastTime.Contains(BotType.Geode) && BuildBot(BotType.Geode).IsValid())
        {
            potentialBuilds.Add(BotType.Geode);
            return new List<BotType>() {BotType.Geode};
        }        
        if (!optionsLastTime.Contains(BotType.Obsidian) && !IsObsidianInfinite(time) && BuildBot(BotType.Obsidian).IsValid())
        {
            potentialBuilds.Add(BotType.Obsidian);
            return new List<BotType>() { BotType.Obsidian };
        }

        // It is not true for the case of clay / ore bots.
        if (!optionsLastTime.Contains(BotType.Clay) && !IsClayInfinite(time) && BuildBot(BotType.Clay).IsValid())
        {
            potentialBuilds.Add(BotType.Clay);
        }
        if (!optionsLastTime.Contains(BotType.Ore) && !IsOreInfinite(time) && BuildBot(BotType.Ore).IsValid())
        {
            potentialBuilds.Add(BotType.Ore);
        }
        
        // If we have no other choice, do nothing. Leaving None in creates a TON
        // of options because IF we were able to buy something and didn't. The
        // next time we can still buy it. Easily disprovable as an option.
        // Really, we should say IF we were able to buy a specific bot last time
        // and didn't, we shouldn't buy the bot next time either.
        return potentialBuilds;
    }

    public bool IsOreInfinite(int time)
    {
        Cost[] costs = { BluePrint.OreRobot, BluePrint.ObsidianRobot, BluePrint.ClayRobot, BluePrint.GeodeRobot };
        int maxOreCost = costs.Select(c => c.Ore).Max();
        // Don't make more bots if I can make the max amount of ore nee
        // for any other bot.

        // Don't make more bots IF, I essentially have infinite ore
        // - If the amount of ore I have cannot be spent before time is up,
        //   there is essentially infinite ore.
        int maxSpend = maxOreCost * time;
        return maxSpend < StockPile.Ore ||  // Not enough time to spend it
               Bots.Ore >= maxOreCost; // Making more than can be spent
    }

    public bool IsClayInfinite(int time)
    {
        Cost[] costs = { BluePrint.OreRobot, BluePrint.ObsidianRobot, BluePrint.ClayRobot, BluePrint.GeodeRobot };
        int maxClayCost = costs.Select(c => c.Clay).Max();
        // Don't make more bots if I can make the max amount of ore nee
        // for any other bot.

        // Don't make more bots IF, I essentially have infinite ore
        // - If the amount of ore I have cannot be spent before time is up,
        //   there is essentially infinite ore.
        int maxSpend = maxClayCost * time;
        return maxSpend < StockPile.Clay || // Not enough time to spend it
               Bots.Clay >= maxClayCost; // Making more than can be spent
    }

    public bool IsObsidianInfinite(int time)
    {
        Cost[] costs = { BluePrint.OreRobot, BluePrint.ObsidianRobot, BluePrint.ClayRobot, BluePrint.GeodeRobot };
        int maxObsidian = costs.Select(c => c.Obsidian).Max();
        // Don't make more bots if I can make the max amount of ore nee
        // for any other bot.

        // Don't make more bots IF, I essentially have infinite ore
        // - If the amount of ore I have cannot be spent before time is up,
        //   there is essentially infinite ore.
        int maxSpend = maxObsidian * time;
        return maxSpend < StockPile.Obsidian || // Not enough time to spend it
               Bots.Obsidian >= maxObsidian; // Making more than can be spent
    }

    public StockPile BuildBot(BotType toBuild)
    {
        Cost forBot = toBuild switch
        {
            BotType.None => new Cost(0, 0, 0),
            BotType.Ore => BluePrint.OreRobot,
            BotType.Clay => BluePrint.ClayRobot,
            BotType.Obsidian => BluePrint.ObsidianRobot,
            BotType.Geode => BluePrint.GeodeRobot
        };
        return StockPile with
        {
            Ore = StockPile.Ore - forBot.Ore,
            Clay = StockPile.Clay - forBot.Clay,
            Obsidian = StockPile.Obsidian - forBot.Obsidian
        };
    }

    public Factory CollectResourcesAndBuild(BotType toBuild, int time)
    {
        StockPile newPile = BuildBot(toBuild);
        newPile = newPile with
        {
            // If it isn't reasonable to build, we say that we have an infinite amount of ore
            Ore = IsOreInfinite(time) ? INF : newPile.Ore + Bots.Ore,
            Clay = IsClayInfinite(time) ? INF : newPile.Clay + Bots.Clay,
            Obsidian = IsObsidianInfinite(time) ? INF : newPile.Obsidian + Bots.Obsidian,
            Geode = newPile.Geode + Bots.Geode,
        };
        Bots newBots = toBuild switch
        {
            BotType.None => Bots,
            BotType.Ore => Bots with { Ore = Bots.Ore + 1 },
            BotType.Clay => Bots with { Clay = Bots.Clay + 1 },
            BotType.Obsidian => Bots with { Obsidian = Bots.Obsidian + 1 },
            BotType.Geode => Bots with { Geode = Bots.Geode + 1 },
            _ => throw new Exception($"Invalid bot type: {toBuild}")
        };
        return this with { StockPile = newPile, Bots = newBots };
    }

}

public enum BotType { None, Ore, Clay, Obsidian, Geode };

public record StockPile(int Ore, int Clay, int Obsidian, int Geode)
{
    public bool IsValid() => Ore >= 0 && Clay >= 0 && Obsidian >= 0 && Geode >= 0;
}
public record Bots(int Ore, int Clay, int Obsidian, int Geode);