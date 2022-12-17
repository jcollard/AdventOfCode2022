// See https://aka.ms/new-console-template for more information
string jets = File.ReadAllText("input.txt").Trim();
Simulator s = new Simulator(jets);
s.InitBoard();
while (s.SetPieces < 2022)
{
    // Console.Clear();
    // Console.WriteLine("\n\n=-=-=-=-=");
    // s.Board.PrintBoard();
    // Console.ReadLine();
    s.Step();

    // Position attempt = s.StepJet();
    // // Console.Clear();
    // Console.WriteLine("\n\n=-=-=-=-=");
    // Console.WriteLine(attempt);
    // s.Board.PrintBoard();
    // Console.ReadLine();

    // s.StepFall();
}
Console.WriteLine(s.Board.HighestPoint + 1);