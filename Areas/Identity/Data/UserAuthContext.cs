using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task4_UserAuth.Models;

namespace Task4_UserAuth.Data;

public class UserAuthContext : IdentityDbContext<ApplicationUser>
{
    public UserAuthContext(DbContextOptions<UserAuthContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>()
            .HasIndex(u => u.Email)
            .IsUnique();

    }
}
