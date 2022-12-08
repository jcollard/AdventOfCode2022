public class ViewScores
{
    public int North {get; set;} = 0;
    public int East {get; set;} = 0;
    public int South {get; set;} = 0;
    public int West {get; set;} = 0;
    public int ScenicScore => North * East * South * West;
}