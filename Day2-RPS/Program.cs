// See https://aka.ms/new-console-template for more information
string[] data = File.ReadAllLines("input.txt");
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

Shape CharToShape(char ch)
{
    return ch switch {
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
    return shape switch {
        Shape.Rock => 1,
        Shape.Paper => 2,
        Shape.Scissors => 3,
        _ => throw new Exception($"Invalid shape...  {shape}")
    };
}

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
