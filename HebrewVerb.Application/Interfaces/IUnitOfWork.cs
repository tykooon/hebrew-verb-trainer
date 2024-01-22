using HebrewVerb.Application.Interfaces.Repositories;
using HebrewVerb.Domain.Interfaces;

namespace HebrewVerb.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    //IFutureRepository FutureRepository { get; }
    IGizraRepository GizraRepository { get; }
    //IImperativeRepository ImperativeRepository { get; }
    //IPastRepository PastRepository { get; }
    //IPresentRepository PresentRepository { get; }
    IShoreshRepository ShoreshRepository { get; }
    IVerbModelRepository VerbModelRepository { get; }
    IVerbRepository VerbRepository { get; }
    IWordFormRepository WordFormRepository { get; }

    IFilterSnapshotRepository FilterSnapshotRepository { get; }

    Task CommitAsync();
}
