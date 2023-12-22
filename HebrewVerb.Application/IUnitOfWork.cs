using HebrewVerb.Application.Identity;
using HebrewVerb.Application.Interfaces;

namespace HebrewVerb.Application;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepo { get; }
    IRoleRepository RoleRepo { get; }
    IVerbRepository VerbRepo { get; }
    IShoreshRepository ShoreshRepo { get; }
    IVerbModelRepository VerbModelRepo { get; }

    void Commit();
}
