using cse325_team1.Components;
using cse325_team1.Data.Services;
using cse325_team1.Data.Models;
using cse325_team1.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<IEventService, JsonEventService>();

// Your app services
builder.Services.AddScoped<cse325_team1.Data.Services.AuthService>();
builder.Services.AddScoped<SessionStore>();
builder.Services.AddScoped<AppAuthStateProvider>();

// If you’re using AuthenticationStateProvider pattern:
builder.Services.AddScoped<Microsoft.AspNetCore.Components.Authorization.AuthenticationStateProvider>(
    sp => sp.GetRequiredService<AppAuthStateProvider>());

builder.Services.AddAuthorizationCore();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=journal.db"));

// ✅ Custom Blazor auth (register ONCE)
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AppAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, AppAuthStateProvider>();

builder.Services.AddScoped<SessionStore>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
