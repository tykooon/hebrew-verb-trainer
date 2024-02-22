using Ardalis.Result;
using HebrewVerb.Application.Exceptions;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using MediatR;

namespace HebrewVerb.Application.Feature.VerbModels.Commands;

public record DeleteVerbModelCommand(int Id) : IRequest<Result>;

public class DeleteVerbModelCommandHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<DeleteVerbModelCommand, Result>
{
    public async Task<Result> Handle(DeleteVerbModelCommand request, CancellationToken cancellationToken)
    {
        var verbModel = _unitOfWork.VerbModelRepository.GetById(request.Id);
        if (verbModel == null)
        {
            return Result.NotFound($"VerbModel with id {request.Id} doesn't exist.");
        }

        try
        {
            _unitOfWork.VerbModelRepository.Delete(verbModel);
            await _unitOfWork.CommitAsync();
        }
        catch (UpdateException ex)
        {
            return Result.Error(ex.Message);
        }
        return Result.Success();
    }
}
