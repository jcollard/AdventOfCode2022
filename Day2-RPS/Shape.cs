public enum Shape
{
    Rock, Paper, Scissors
}

public static class ShapeExtensions
{
    public static bool Defeats(this Shape p0, Shape p1)
    {
        return (p0 == Shape.Rock && p1 == Shape.Scissors) ||
               (p0 == Shape.Scissors && p1 == Shape.Paper) ||
               (p0 == Shape.Paper && p1 == Shape.Rock);
    }

    public static Shape Defeats(this Shape shape)
    {
        return shape switch {
            Shape.Rock => Shape.Scissors,
            Shape.Paper => Shape.Rock,
            Shape.Scissors => Shape.Paper,
            _ => throw new Exception($"Could not find shape that defeats {shape}")
        };
    }

    public static Shape LosesTo(this Shape shape)
    {
        return shape switch {
            Shape.Rock => Shape.Paper,
            Shape.Paper => Shape.Scissors,
            Shape.Scissors => Shape.Rock,
            _ => throw new Exception($"Could not find shape that defeats {shape}")
        };
    }

    public static bool Draws(this Shape p0, Shape p1) => p0 == p1;

}