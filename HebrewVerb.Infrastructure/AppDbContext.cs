using HebrewVerb.Application.Entities;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Infrastructure;

public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>, IAppDbContext
{
    public DbSet<Verb> Verbs { get; set; }
    public DbSet<Shoresh> Shoreshes { get; set; }
    public DbSet<WordForm> WordForms { get; set; }
    public DbSet<Past> Pasts { get; set; }
    public DbSet<Present> Presents { get; set; }
    public DbSet<Future> Futures { get; set; }
    public DbSet<Imperative> Imperatives { get; set; }
    public DbSet<AppFilter> FilterSnapshots { get; set; }
    public DbSet<Translation> Translations { get; set; }
    public DbSet<Preposition> Prepositions { get; set; }

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
        //optionsBuilder.UseMySQL(opt => 
        //    opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));

        optionsBuilder.UseSqlite();
    }

}
