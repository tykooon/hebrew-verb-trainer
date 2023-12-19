using HebrewVerb.Application;
using HebrewVerb.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Infrastructure;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
{
    public DbSet<Verb> Verbs { get; set; }
    public DbSet<Shoresh> Shoreshes { get; set; }
    public DbSet<Gizra> Gizras { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Shoresh>()
            .HasMany(v => v.Gizras)
            .WithMany(g => g.Shoreshes);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=appDb.db");
    }
}
