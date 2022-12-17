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
    // Console.WriteLine(s.Board.HighestPoint + 1);
    if(s.Step() == false)
    {
        break;
    }
    // Position attempt = s.StepJet();
    // // Console.Clear();
    // Console.WriteLine("\n\n=-=-=-=-=");
    // Console.WriteLine(attempt);
    // s.Board.PrintBoard();
    // Console.ReadLine();

    // s.StepFall();
}
Console.WriteLine(s.Board.HighestPoint + 1);
// ulong val = 666_666_666_665 * 27 + 8 + 36;
// Console.WriteLine(val);