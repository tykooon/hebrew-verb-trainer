using HebrewVerb.Application.Entities;
using HebrewVerb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Application.Interfaces;

// Perhaps this interface is not necessary, due to access to Database via UnitOfWork
public interface IAppDbContext
{
    public DbSet<Verb> Verbs { get; }
    public DbSet<Shoresh> Shoreshes { get; }
    public DbSet<WordForm> WordForms { get; }
    public DbSet<Imperative> Imperatives { get; }
    public DbSet<Past> Pasts { get; }
    public DbSet<Present> Presents { get; }
    public DbSet<Future> Futures { get; }
    public DbSet<Translation> Translations { get; }
    public DbSet<Preposition> Prepositions { get; }

    public DbSet<AppFilter> FilterSnapshots { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}