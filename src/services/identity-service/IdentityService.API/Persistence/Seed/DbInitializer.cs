using IdentityService.API.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.API.Persistence.Seed;

public static class DbInitializer
{
    public static void Seed(this AppDbContext dbContext)
    {
        dbContext.Database.EnsureCreated();

        if (dbContext.Users.Any())
        {
            return;
        }
        User adminUser = new()
        {
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = "admin",
            NormalizedUserName = "admin".Normalize().ToUpperInvariant(),
            Name = "admin"
        };
        PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
        adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "admin123456");
        dbContext.Users.Add(adminUser);
        dbContext.SaveChanges();
    }
}
