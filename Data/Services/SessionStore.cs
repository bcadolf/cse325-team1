namespace cse325_team1.Data.Services;

public class SessionStore
{
    // simple in-memory session (per server instance)
    // good for school projects; for production store in DB/Redis/cookies
    public int? UserId { get; private set; }
    public string? Username { get; private set; }

    public void Set(int userId, string username)
    {
        UserId = userId;
        Username = username;
    }

    public void Clear()
    {
        UserId = null;
        Username = null;
    }

    public bool IsLoggedIn => UserId.HasValue;
}
