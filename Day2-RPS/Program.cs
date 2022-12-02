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
        Shape opponent = pair[0][0].ToShape();
        Outcome outcome = pair[1][0].ToOutcome();
        Shape player = outcome switch {
            Outcome.Win => opponent.LosesTo(),
            Outcome.Draw => opponent,
            Outcome.Lose => opponent.Defeats(),
            _ => throw new Exception($"Could not determine outcome")
        };
        Console.WriteLine($"Round: {roundNumber++}");
        Console.WriteLine($"Opponent chooses {opponent}, we want to {outcome}, so we choose {player}.");
        totalScore += player.GetValue();
        totalScore += player.PointsAgainst(opponent);
    }
    Console.WriteLine(totalScore);
}

void Part1()
{
    int totalScore = 0;
    foreach (string round in data)
    {
        Shape[] shapes = round.Split().Select(s => s[0].ToShape()).ToArray();
        Shape opponent = shapes[0];
        Shape player = shapes[1];
        totalScore += player.GetValue();
        totalScore += player.PointsAgainst(opponent);
    }
    Console.WriteLine(totalScore);
}