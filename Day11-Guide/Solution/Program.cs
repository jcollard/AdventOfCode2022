List<Monkey> friends = Monkey.ParseAll(File.ReadAllText("example.txt"));

for(int i = 0; i < 20; i++)
{
    Monkey.ExecuteRound(friends);
}
friends.Sort((a, b) => b.InspectionCount - a.InspectionCount);
int monkeyBusiness = friends[0].InspectionCount * friends[1].InspectionCount;
Console.WriteLine(monkeyBusiness);