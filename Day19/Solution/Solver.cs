public record Solver(BluePrint BluePrint)
{

    // State => Max Geodes
    public Dictionary<State, int> Memoized = new();

    public int Solve(int time)
    {
        Best = new State(time, new Factory(BluePrint, new StockPile(0, 0, 0, 0), new Bots(1, 0, 0, 0)));
        return Solve(Best, new HashSet<BotType>());
    }

    private State Best;
    private int MaxSearchSpace = 5_000_000;

    public int Solve(State state, HashSet<BotType> optionsLastTime)
    {
        if (Memoized.Count > MaxSearchSpace)
        {
            Console.WriteLine("Reached max search space, returning best.");
            Memoized[state] = state.Factory.StockPile.Geode;
            return Best.Factory.StockPile.Geode;
        }

        if (Memoized.TryGetValue(state, out int result))
        {
            return result;
        }

        if (state.TimeRemaining == 0)
        {
            if (state.Factory.StockPile.Geode > Best.Factory.StockPile.Geode)
            {
                Best = state;
                Console.WriteLine($"{Memoized.Count} Nodes");
                Console.WriteLine($"Best so far: {Best}");
            }

            Memoized[state] = state.Factory.StockPile.Geode;
            return state.Factory.StockPile.Geode;
        }

        int max = state.Factory.StockPile.Geode;
        foreach ((BotType choice, Factory f) in state.Factory.Outcomes(state.TimeRemaining, optionsLastTime))
        {
            HashSet<BotType> options = state.Factory.ReasonableChoices(state.TimeRemaining, optionsLastTime).ToHashSet();
            
            State nextState = new State(state.TimeRemaining - 1, f);
            if (choice == BotType.None)
            {
                // If we chose None last time, do not take any of the options
                max = Math.Max(max, Solve(nextState, options));   
            }
            else
            {
                max = Math.Max(max, Solve(nextState, optionsLastTime));   
            }
        }
        Memoized[state] = max;
        // Console.WriteLine($"{state} => {max}");
        return max;
    }

}

public record State(int TimeRemaining, Factory Factory);