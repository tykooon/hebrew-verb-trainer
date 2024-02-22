using Ardalis.Result;
using HebrewVerb.Application.Exceptions;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;
using HebrewVerb.SharedKernel.Extensions;
using MediatR;

namespace HebrewVerb.Application.Feature.Gizras.Commands;

public record UpdateGizraCommand(GizraDto GizraDto) : IRequest<Result>;

public class UpdateGizraCommandHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<UpdateGizraCommand, Result>
{
    public async Task<Result> Handle(UpdateGizraCommand request, CancellationToken cancellationToken)
    {
        var gizra = _unitOfWork.GizraRepository.GetById(request.GizraDto.Id);
        if (gizra == null)
        {
            return Result.NotFound($"Gizra with id {request.GizraDto.Id} doesn't exist.");
        }

        gizra.Update(
            request.GizraDto.Name,
            request.GizraDto.Description,
            [ ..request.GizraDto.Binyans.GetBinyans()]);
        try
        {
            await _unitOfWork.CommitAsync();
        }
        catch (UpdateException ex)
        {
            return Result.Error(ex.Message);
        }
        return Result.Success();
    }
}
