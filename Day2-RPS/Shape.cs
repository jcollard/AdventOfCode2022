public enum Shape
{
    Rock, Paper, Scissors
}

public static class ShapeExtensions
{
    /// <summary>
    /// Returns true if this shape would defeat the opponents shape.
    /// </summary>
    public static bool Defeats(this Shape player, Shape opponent)
    {
        return (player == Shape.Rock && opponent == Shape.Scissors) ||
               (player == Shape.Scissors && opponent == Shape.Paper) ||
               (player == Shape.Paper && opponent == Shape.Rock);
    }

    /// <summary>
    /// Determines which shape loses to this shape.
    /// </summary>
    public static Shape Defeats(this Shape shape)
    {
        return shape switch
        {
            Shape.Rock => Shape.Scissors,
            Shape.Paper => Shape.Rock,
            Shape.Scissors => Shape.Paper,
            _ => throw new Exception($"Could not find shape that defeats {shape}")
        };
    }

    /// <summary>
    /// Determines which shape would defeat this shape.
    /// </summary>
    public static Shape LosesTo(this Shape shape)
    {
        return shape switch
        {
            Shape.Rock => Shape.Paper,
            Shape.Paper => Shape.Scissors,
            Shape.Scissors => Shape.Rock,
            _ => throw new Exception($"Could not find shape that defeats {shape}")
        };
    }

    /// <summary>
    /// Determines the number of points that a player scores
    /// against an opponent.
    /// </summary>
    public static int PointsAgainst(this Shape playerShape, Shape opponentShape)
    {
        if (playerShape.Defeats(opponentShape))
        {
            return 6;
        }

        if (playerShape.Draws(opponentShape))
        {
            return 3;
        }

        return 0;
    }

    /// <summary>
    /// Determines the value of this shape
    /// </summary>
    public static int GetValue(this Shape shape)
    {
        return shape switch
        {
            Shape.Rock => 1,
            Shape.Paper => 2,
            Shape.Scissors => 3,
            _ => throw new Exception($"Invalid shape...  {shape}")
        };
    }

    /// <summary>
    /// Converts this character to a Shape. Throws an exception
    /// if the character is not one of ('A','B','C','X','Y','Z')
    /// </summary>
    public static Shape ToShape(this char ch)
    {
        return ch switch
        {
            'A' => Shape.Rock,
            'B' => Shape.Paper,
            'C' => Shape.Scissors,
            'X' => Shape.Rock,
            'Y' => Shape.Paper,
            'Z' => Shape.Scissors,
            _ => throw new Exception($"Invalid shape... {ch}")
        };
    }

    /// <summary>
    /// Returns true if this shape draws against the opponents shape.
    /// </summary>
    public static bool Draws(this Shape player, Shape opponent) => player == opponent;

}