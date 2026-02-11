using cse325_team1.Components;
using cse325_team1.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// EF Core (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=journal.db"));

var app = builder.Build();

// Ensure DB exists (ok for dev; for real apps use migrations)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();        // ✅ routing first

// If you later add real auth:
// app.UseAuthentication();
// app.UseAuthorization();

app.UseAntiforgery();    // ✅ after routing (+ after auth if you add it)

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
