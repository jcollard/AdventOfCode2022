string[] lines = File.ReadAllLines("sample.txt");
int totalScore = 0;
foreach (string line in lines)
{
    string myShape = CharToShape(line[2]);
    string opponentShape = CharToShape(line[0]);
    int roundScore = ScoreRound(myShape, opponentShape);
    Console.WriteLine($"{myShape} vs {opponentShape} = {roundScore}");
    totalScore += roundScore;
}
Console.WriteLine($"Total Score: {totalScore}");

string CharToShape(char ch)
{
    return ch switch {
        'A' => "Rock",
        'B' => "Paper",
        'C' => "Scissors",
        'X' => "Rock",
        'Y' => "Paper",
        'Z' => "Scissors",
        _ => throw new Exception($"Invalid character {ch}")
    };
}

int GetShapeValue(string shape)
{
    return shape switch {
        "Rock" => 1,
        "Paper" => 2,
        "Scissors" => 3,
        _ => throw new Exception($"Invalid shape {shape}")
    };
}

bool IsWin(string myShape, string opponentShape)
{
    return (myShape, opponentShape) switch {
        ("Rock", "Scissors") => true,
        ("Scissors", "Paper") => true,
        ("Paper", "Rock") => true,
        _ => false
    };
}

bool IsDraw(string myShape, string opponentShape)
{
    return myShape == opponentShape;
}

int ScoreRound(string myShape, string opponentShape)
{
    int points = GetShapeValue(myShape);
    if (IsWin(myShape, opponentShape))
    {
        points += 6;
    }
    else if (IsDraw(myShape, opponentShape))
    {
        points += 3;
    }
    return points;
}