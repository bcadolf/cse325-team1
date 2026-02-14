using System.Text.Json;
using cse325_team1.Data.Models;

namespace cse325_team1.Data.Services;

public class JsonEventService : IEventService
{
    private readonly IWebHostEnvironment _env;

    public JsonEventService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<List<Event>> GetEventsAsync()
    {
        var path = Path.Combine(_env.WebRootPath, "Data", "TempData", "events.json");

        var json = await File.ReadAllTextAsync(path);

        return JsonSerializer.Deserialize<List<Event>>(json)
               ?? new List<Event>();
    }
}
