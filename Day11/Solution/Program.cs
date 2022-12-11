string[] monkeys = File.ReadAllText("input.txt").Split("\n\n");

Part2();

void Part2()
{
    List<Monkey> friends = monkeys.Select(Monkey.Parse).ToList();
    int LCM = friends.Select(m => m.Divisor).Aggregate(1, (a, b) => a * b);
    Console.WriteLine(LCM);
    List<Monkey> enemies = friends.Select(m => new MeanMonkey(m, LCM) as Monkey).ToList();
    for (int round = 0; round < 10000; round++)
    {
        // if (round == 1 || round == 20 || round == 1000)
        // {
        //     Console.WriteLine($"== After round {round} ==");
        //     DisplayInfo(enemies);
        // }
        Round(enemies);
        
    }
    List<Monkey> ordered = enemies.OrderByDescending((m) => m.InspectionCount).ToList();
    long monkeyBusiness = ordered[0].InspectionCount * ordered[1].InspectionCount;
    Console.WriteLine($"Total monkey business: {monkeyBusiness}");
}

void DisplayInfo(List<Monkey> monkeys)
{
    foreach (Monkey m in monkeys)
    {
        Console.WriteLine($"Monkey {m.ID} inspected items {m.InspectionCount} times.");
    }
}

void Part1()
{
    List<Monkey> friends = monkeys.Select(Monkey.Parse).ToList();
    for (int round = 0; round < 20; round++)
    {
        Round(friends);
    }
    List<Monkey> ordered = friends.OrderByDescending((m) => m.InspectionCount).ToList();
    long monkeyBusiness = ordered[0].InspectionCount * ordered[1].InspectionCount;
    Console.WriteLine($"Total monkey business: {monkeyBusiness}");
}

void Round(List<Monkey> friends)
{
    foreach (Monkey m in friends)
    {
        while (m.InspectItem(friends)) ;
    }
}
