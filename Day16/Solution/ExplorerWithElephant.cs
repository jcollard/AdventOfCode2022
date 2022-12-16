public record ExplorerWithElephant(Cave Cave)
{
    public Dictionary<((int, string), (int, string), ValveState), int> Results = new();

    public int Explore(int time)
    {

        ValveState initial = new(0);
        LastTick = Environment.TickCount;
        (int, string) explorer = (time, "AA");
        (int, string) elephant = (time, "AA");
        return Explore(explorer, elephant, initial);
    }

    private int LastTick = 0;

    public int Explore((int time, string position) e0, (int time, string position) e1, ValveState state)
    {
        ((int, string), (int, string), ValveState) key = (e0, e1, state);
        if (Results.ContainsKey(key))
        {
            // Console.WriteLine("Cache hit!");
            return Results[key];
        }

        // Ensure the explorer with the next movement goes next
        if (e1.time > e0.time)
        {
            Results[key] = Explore(e1, e0, state);
            return Results[key];
        }

        // e0 should act
        string[] options = Cave.Nodes.Keys
        .Where(n => !state.IsOn(Cave.Nodes[n])) // Skip rooms with valve on
        .Where(n =>
        {
            int timeUsed = Cave.TravelTime[(e0.position, n)] + 1;
            int timeRemaining = e0.time - timeUsed;
            // Skip rooms that are too far away or have no benefit
            return timeRemaining > 0 && Cave.Benefit(timeRemaining, n, state) > 0;
        })
        .ToArray();
        List<int> outcomes = new() { 0 };
        foreach (string n in options)
        {
            int timeUsed = Cave.TravelTime[(e0.position, n)] + 1;
            int timeRemaining = e0.time - timeUsed;
            int benefit = Cave.Benefit(timeRemaining, n, state);
            ValveState newState = state.TurnOn(Cave.Nodes[n]);
            (int, string) newExplorer = (timeRemaining, n);
            outcomes.Add(benefit + Explore(newExplorer, e1, newState));
        }
        Results[key] = outcomes.Max();
        return Results[key];

    }

}