using HebrewVerb.Application.Entities;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Domain.Entities;
using HebrewVerb.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Infrastructure;

public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>, IAppDbContext
{
    public DbSet<Verb> Verbs { get; set; }
    public DbSet<Shoresh> Shoreshes { get; set; }
    public DbSet<VerbModel> VerbModels { get; set; }
    public DbSet<WordForm> WordForms { get; set; }
    public DbSet<Gizra> Gizras { get; set; }
    public DbSet<Past> Pasts { get; set; }
    public DbSet<Present> Presents { get; set; }
    public DbSet<Future> Futures { get; set; }
    public DbSet<Imperative> Imperatives { get; set; }
    public DbSet<FilterSnapshot> FilterSnapshots { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public AppDbContext() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=appDb.db");
    }
}
