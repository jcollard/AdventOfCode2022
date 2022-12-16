public record Explorer(Cave Cave)
{
    public Dictionary<(int, string, ValveState), int> Results = new();

    public int Explore(int time)
    {

        ValveState initial = new(0);
        LastTick = Environment.TickCount;
        return Explore(time, "AA", initial);
    }

    private int LastTick = 0;

    public int Explore(int time, string position, ValveState state)
    {
        (int, string, ValveState) key = (time, position, state);
        if (Results.ContainsKey(key))
        {
            // Console.WriteLine("Cache hit!");
            return Results[key];
        }

        List<int> outcomes = new() { 0 };
        foreach (string n in Cave.Nodes.Keys)
        {
            if (state.IsOn(Cave.Nodes[n])) continue;
            int timeUsed = Cave.TravelTime[(position, n)] + 1;
            int timeRemaining = time - timeUsed;
            if (timeRemaining <= 0) continue;
            int benefit = Cave.Benefit(timeRemaining, n, state);
            if (benefit == 0) continue;
            // Console.WriteLine($"Benefit of turning on {n} with {timeRemaining} time would be {benefit}");
            ValveState newState = state.TurnOn(Cave.Nodes[n]);
            outcomes.Add(benefit + Explore(timeRemaining, n, newState));
        }
        Results[key] = outcomes.Max();
        if (Results.Count % 100_000 == 0)
        {
            int CurrentTicks = Environment.TickCount;
            int TotalTicks = CurrentTicks - LastTick;
            Console.WriteLine($"{Results.Count} entries @ {TotalTicks}");
        }
        // Console.WriteLine($"Enter {Results.Count}: {time} | {position} | {state}");
        return Results[key];
    }

}