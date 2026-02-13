using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace cse325_team1.Services;

public class AppAuthStateProvider : AuthenticationStateProvider
{
    private readonly SessionStore _session;

    public AppAuthStateProvider(SessionStore session)
    {
        _session = session;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsIdentity identity;

        if (_session.IsLoggedIn && _session.UserId is not null && _session.Username is not null)
        {
            identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, _session.UserId.Value.ToString()),
                new Claim(ClaimTypes.Name, _session.Username)
            }, authenticationType: "Custom");
        }
        else
        {
            identity = new ClaimsIdentity();
        }

        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
    }

    public void Notify() => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
}
