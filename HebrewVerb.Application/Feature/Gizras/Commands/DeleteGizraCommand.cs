using Ardalis.Result;
using HebrewVerb.Application.Exceptions;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using MediatR;

namespace HebrewVerb.Application.Feature.Gizras.Commands;

public record DeleteGizraCommand(int Id) : IRequest<Result>;

public class DeleteGizraCommandHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<DeleteGizraCommand, Result>
{
    public async Task<Result> Handle(DeleteGizraCommand request, CancellationToken cancellationToken)
    {
        var gizra = _unitOfWork.GizraRepository.GetById(request.Id);
        if (gizra == null)
        {
            return Result.NotFound($"Gizra with id {request.Id} doesn't exist.");
        }

        try
        {
            _unitOfWork.GizraRepository.Delete(gizra);
            await _unitOfWork.CommitAsync();
        }
        catch (UpdateException ex)
        {
            return Result.Error(ex.Message);
        }
        return Result.Success();
    }
}
