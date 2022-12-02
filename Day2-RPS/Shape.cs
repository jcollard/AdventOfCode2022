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

    public static bool Draws(this Shape p0, Shape p1) => p0 == p1;

}