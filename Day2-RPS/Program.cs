// See https://aka.ms/new-console-template for more information
string[] data = File.ReadAllLines("input.txt");

Part2();

void Part2()
{
    int totalScore = 0;
    int roundNumber = 1;
    foreach (string round in data)
    {
        string[] pair = round.Split();
        Shape opponent = CharToShape(pair[0][0]);
        Outcome outcome = CharToOutcome(pair[1][0]);
        Shape player = outcome switch {
            Outcome.Win => opponent.LosesTo(),
            Outcome.Draw => opponent,
            Outcome.Lose => opponent.Defeats(),
            _ => throw new Exception($"Could not determine outcome")
        };
        Console.WriteLine($"Round: {roundNumber++}");
        Console.WriteLine($"Opponent chooses {opponent}, we want to {outcome}, so we choose {player}.");
        totalScore += GetShapeValue(player);
        totalScore += GetResults(player, opponent);
    }
    Console.WriteLine(totalScore);
}

void Part1()
{
    int totalScore = 0;
    foreach (string round in data)
    {
        Shape[] shapes = round.Split().Select(s => CharToShape(s[0])).ToArray();
        Shape opponent = shapes[0];
        Shape player = shapes[1];
        totalScore += GetShapeValue(player);
        totalScore += GetResults(player, opponent);
    }
    Console.WriteLine(totalScore);
}

Outcome CharToOutcome(char ch)
{
    return ch switch 
    {
        'X' => Outcome.Lose,
        'Y' => Outcome.Draw,
        'Z' => Outcome.Win,
        _ => throw new Exception($"Could not convert char {ch} to Outcome.")
    };
}

Shape CharToShape(char ch)
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

int GetShapeValue(Shape shape)
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
/// Given a shape for two players (p0 and p1), returns
/// the number of points that p0 would receive.
/// </summary>
int GetResults(Shape p0, Shape p1)
{
    if (p0.Defeats(p1))
    {
        return 6;
    }

    if (p0.Draws(p1))
    {
        return 3;
    }

    return 0;
}

enum Outcome
{
    Win, Draw, Lose
}