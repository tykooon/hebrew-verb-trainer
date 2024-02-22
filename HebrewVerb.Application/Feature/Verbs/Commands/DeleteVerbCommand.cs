using Ardalis.Result;
using HebrewVerb.Application.Exceptions;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using MediatR;

namespace HebrewVerb.Application.Feature.Verbs.Commands;

public record DeleteVerbCommand(int Id) : IRequest<Result>;

public class DeleteVerbModelCommandHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<DeleteVerbCommand, Result>
{
    public async Task<Result> Handle(DeleteVerbCommand request, CancellationToken cancellationToken)
    {
        var verb = _unitOfWork.VerbRepository.GetById(request.Id);
        if (verb == null)
        {
            return Result.NotFound($"VerbModel with id {request.Id} doesn't exist.");
        }

        try
        {
            _unitOfWork.VerbRepository.Delete(verb);
            await _unitOfWork.CommitAsync();
        }
        catch (RepositoryException ex)
        {
            return Result.Error(ex.Message);
        }
        return Result.Success();
    }
}
