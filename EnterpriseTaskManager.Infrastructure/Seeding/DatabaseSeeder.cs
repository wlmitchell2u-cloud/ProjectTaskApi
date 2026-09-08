using EnterpriseTaskManager.Domain.Entities;
using EnterpriseTaskManager.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using System.Text;

namespace EnterpriseTaskManager.Infrastructure.Seeding;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var provider = scope.ServiceProvider;
        var db = provider.GetRequiredService<AppDbContext>();

        // If there are already users, assume seeded
        if (db.Users.Any()) return;

        var now = DateTime.UtcNow;

        var users = new List<User>
        {
            new User
            {
                Email = "admin@local",
                FullName = "Admin User",
                DisplayName = "Admin",
                Role = "Admin",
                PasswordHash = HashPassword("Password123!"),
                CreatedAtUtc = now,
                CreatedBy = "Seeder"
            },
            new User
            {
                Email = "user@local",
                FullName = "Regular User",
                DisplayName = "User",
                Role = "User",
                PasswordHash = HashPassword("Password123!"),
                CreatedAtUtc = now,
                CreatedBy = "Seeder"
            }
        };

        db.Users.AddRange(users);
        await db.SaveChangesAsync();
    }

    private static string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
