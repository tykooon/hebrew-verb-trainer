using HebrewVerb.Application.Exceptions;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Interfaces.Repositories;
using HebrewVerb.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private bool _isDisposed;
    private AppDbContext _context;

    private IVerbRepository? _verbRepo;
    private IShoreshRepository? _shoreshRepo;
    private IGizraRepository? _gizraRepo;
    private IVerbModelRepository? _verbModelRepo;
    private IWordFormRepository? _wordFormRepo;
    private IFilterRepository? _filterRepo;
    private IPrepositionRepository? _prepositionRepo;
    private ITranslationRepository? _translationRepo;

    private AppDbContext Context => _context ??= new AppDbContext();

    public IVerbRepository VerbRepository => _verbRepo ??= new VerbRepository(Context);
    public IShoreshRepository ShoreshRepository => _shoreshRepo ??= new ShoreshRepository(Context);
    public IGizraRepository GizraRepository => _gizraRepo ??= new GizraRepository(Context);
    public IVerbModelRepository VerbModelRepository => _verbModelRepo ??= new VerbModelRepository(Context);
    public IWordFormRepository WordFormRepository => _wordFormRepo ??= new WordFormRepository(Context);
    public IFilterRepository FilterRepository => _filterRepo ??= new FilterRepository(Context);
    public IPrepositionRepository PrepositionRepository => _prepositionRepo ??= new PrepositionRepository(Context);
    public ITranslationRepository TranslationRepository => _translationRepo ??= new TranslationRepository(Context);

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task CommitAsync()
    {
        if (_context is null)
        {
            return;
        }

        if (_isDisposed)
        {
            throw new ObjectDisposedException("UnitOfWork");
        }

        try
        {
            await Context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException(ex.Entries.Select(x => x.Entity.ToString() ?? ""));
        }
        catch (DbUpdateException ex)
        {
            throw new UpdateException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Commit error.", ex);
        }
    }

    public void Dispose()
    {
        if (_context is null || _isDisposed)
        {
            return;
        }

        _context.Dispose();
        _isDisposed = true;
    }
}
