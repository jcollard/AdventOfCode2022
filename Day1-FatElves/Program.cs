// See https://aka.ms/new-console-template for more information
string[] data = File.ReadAllLines("puzzle_input.txt");

Part1(data);

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