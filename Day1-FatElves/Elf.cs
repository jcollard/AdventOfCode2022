public class Elf
{
    public List<int> Calories;

    public Elf(int[] calories)
    {
        this.Calories = new ();
        foreach (int item in calories)
        {
            this.Calories.Add(item);
        }
    }

    public void Add(int snack)
    {
        this.Calories.Add(snack);
    }

    public int TotalCalories()
    {
        int total = 0;
        foreach (int item in this.Calories)
        {
            total += item;
        }
        return total;
    }
}