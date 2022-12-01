// See https://aka.ms/new-console-template for more information
string[] data = File.ReadAllLines("puzzle_input.txt");


Elf elf0 = new Elf(new int[]{1000, 2000, 3500});
Elf elf1 = new Elf(new int[]{5333, 27});

Console.WriteLine(elf0.TotalCalories());
Console.WriteLine(elf1.TotalCalories());


int TotalCalories(Elf elf)
{
    int total = 0;
    foreach (int item in elf.Calories)
    {
        total += item;
    }
    return total;
}


















void ProcessData(string[] data, Action<int> onElfCount)
{
    int calorieCount = 0;
    foreach (string line in data)
    {
        string cleanLine = line.Trim();
        if (cleanLine == string.Empty)
        {
            Console.WriteLine($"Last Elf's Calories: {calorieCount}");
            onElfCount.Invoke(calorieCount);
            calorieCount = 0;
        }
        else
        {
            calorieCount += int.Parse(line);
        }
    }
}

void Part2(string[] data)
{
    List<int> allCalories = new ();
    ProcessData(data, (calorieCount) => allCalories.Add(calorieCount));
    allCalories.Sort();
    List<int> lastEl = allCalories.GetRange(allCalories.Count - 3,3);
    int total = lastEl.Aggregate((a, b) => a + b);
    Console.WriteLine($"The sum of the top 3 calorie carriers is {total} calories.");
}

/// <summary>
/// Calculates the elf with the most calories
/// </summary>
void Part1(string[] data)
{
    int maxCalories = int.MinValue;
    ProcessData(data, (calorieCount) => maxCalories = Math.Max(maxCalories, calorieCount));
    Console.WriteLine($"The fattest elf has {maxCalories} calories");
}