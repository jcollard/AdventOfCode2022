public enum Outcome
{
    Win, Draw, Lose
}

public static class OutcomeExtensions
{
    /// <summary>
    /// Converts this character to an Outcome. Throws an exception
    /// if char is not one of ('X', 'Y', 'Z')
    /// </summary>
    public static Outcome ToOutcome(this char ch)
    {
        return ch switch
        {
            'X' => Outcome.Lose,
            'Y' => Outcome.Draw,
            'Z' => Outcome.Win,
            _ => throw new Exception($"Could not convert char {ch} to Outcome.")
        };
    }
}