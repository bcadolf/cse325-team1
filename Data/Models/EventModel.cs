namespace cse325_team1.Data.Models;

public class EventModel
{
    public string title { get; set; }
    public DateTime dateTime { get; set; } = DateTime.Now;
    public string description { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string category { get; set; }
    public string location { get; set; }
}