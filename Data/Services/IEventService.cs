using cse325_team1.Data.Models; 

namespace cse325_team1.Data.Services;

public interface IEventService
{
    Task<List<EventModel>> GetEventsAsync();
}