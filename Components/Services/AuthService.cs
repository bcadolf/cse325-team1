using cse325_team1.Data;
using cse325_team1.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace cse325_team1.Services;

public class AuthService
{
    private readonly AppDbContext _db;
    private readonly SessionStore _session;
    private readonly AppAuthStateProvider _auth;

    public AuthService(AppDbContext db, SessionStore session, AppAuthStateProvider auth)
    {
        _db = db;
        _session = session;
        _auth = auth;
    }

    public async Task<(bool ok, string message)> SignupAsync(string username, string password, string firstName, string lastName)
    {
        username = username.Trim();

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            return (false, "Username and password are required.");

        bool exists = await _db.Users.AnyAsync(x => x.Username == username);
        if (exists) return (false, "Username already exists.");

        var user = new UserProfile
        {
            Username = username,
            PasswordHash = HashPassword(password),
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        _session.Set(user.Id, user.Username);
        _auth.Notify();
        return (true, "Account created.");
    }

    public async Task<(bool ok, string message)> LoginAsync(string username, string password)
    {
        username = username.Trim();

        var user = await _db.Users.FirstOrDefaultAsync(x => x.Username == username);
        if (user is null) return (false, "Invalid username or password.");

        if (!VerifyPassword(password, user.PasswordHash))
            return (false, "Invalid username or password.");

        _session.Set(user.Id, user.Username);
        _auth.Notify();
        return (true, "Logged in.");
    }

    public void Logout()
    {
        _session.Clear();
        _auth.Notify();
    }

    // PBKDF2 hash (good enough for school projects)
    private static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);

        return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    private static bool VerifyPassword(string password, string stored)
    {
        var parts = stored.Split('.');
        if (parts.Length != 2) return false;

        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] expected = Convert.FromBase64String(parts[1]);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
        byte[] actual = pbkdf2.GetBytes(32);

        return CryptographicOperations.FixedTimeEquals(actual, expected);
    }
}
