// See https://aka.ms/new-console-template for more information
string[] data = File.ReadAllLines("input.txt");

Part2();

void Part2()
{
    int totalScore = 0;
    int roundNumber = 1;
    foreach (string round in data)
    {
        Shape opponent = round[0].ToShape();
        Outcome outcome = round[2].ToOutcome();
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
        Shape opponent = round[0].ToShape();
        Shape player = round[2].ToShape();
        totalScore += player.GetValue();
        totalScore += player.PointsAgainst(opponent);
    }
    Console.WriteLine(totalScore);
}