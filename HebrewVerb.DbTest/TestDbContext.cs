using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.DbTest;

internal class TestDbContext : DbContext
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
    public DbSet<Translation> Translations { get; set; }
    public DbSet<Preposition> Prepositions { get; set; }

    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
    { }

    public TestDbContext() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=test.db");
        //optionsBuilder.UseMySQL(opt => 
        //    opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
    }


}
