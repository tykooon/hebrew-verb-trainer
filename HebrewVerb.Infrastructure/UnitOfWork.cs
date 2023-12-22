using HebrewVerb.Application;
using HebrewVerb.Application.Identity;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Infrastructure.Identity;
using HebrewVerb.Infrastructure.Repositories;

namespace HebrewVerb.Infrastructure;

// TODO IDisposable
public class UnitOfWork : IUnitOfWork
{
    private bool _isDisposed;
    private AppDbContext _context;

    private IUserRepository? _userRepo;
    private IRoleRepository? _roleRepo;
    private IVerbRepository? _verbRepo;
    private IShoreshRepository? _shoreshRepo;
    private IVerbModelRepository? _gizraRepo;
    
    private AppDbContext Context => _context ??= new AppDbContext();

    public IUserRepository UserRepo => _userRepo ??= new UserRepository(Context);
    public IRoleRepository RoleRepo => _roleRepo ??= new RoleRepository(Context);
    public IVerbRepository VerbRepo => _verbRepo ??= new VerbRepository(Context);
    public IShoreshRepository ShoreshRepo => _shoreshRepo ??= new ShoreshRepository(Context);
    public IVerbModelRepository VerbModelRepo => _gizraRepo ??= new VerbModelRepository(Context);

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public void Commit()
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
            Context.SaveChanges();
        }
        //catch (DbUpdateConcurrencyException ex)
        //{
        //    throw new ConcurrencyException(ex.Entries.Select(x => x.Entity.ToString()));
        //}
        //catch (DbUpdateException ex)
        //{
        //    throw new UpdateException(ex.Message, ex);
        //}
        catch (Exception ex)
        {
            //    throw new RepositoryException("Commit error.", ex);
            throw new Exception("Commit Error", ex);
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
