namespace HebrewVerb.Application;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepo { get; }
    IRoleRepository RoleRepo { get; }
    IVerbRepository VerbRepo { get; }
    IShoreshRepository ShoreshRepo { get; }
    IGizraRepository GizraRepo { get; }

    void Commit();
}
