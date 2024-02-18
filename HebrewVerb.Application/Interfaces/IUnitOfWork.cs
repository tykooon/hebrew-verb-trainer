using HebrewVerb.Application.Interfaces.Repositories;

namespace HebrewVerb.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGizraRepository GizraRepository { get; }
    IPrepositionRepository PrepositionRepository { get; }
    ITranslationRepository TranslationRepository { get; }
    IShoreshRepository ShoreshRepository { get; }
    IVerbModelRepository VerbModelRepository { get; }
    IVerbRepository VerbRepository { get; }
    IWordFormRepository WordFormRepository { get; }

    IFilterRepository FilterRepository { get; }

    Task CommitAsync();
}
