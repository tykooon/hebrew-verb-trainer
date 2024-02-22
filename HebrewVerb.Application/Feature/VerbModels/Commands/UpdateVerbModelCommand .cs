using Ardalis.Result;
using HebrewVerb.Application.Exceptions;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.SharedKernel.Extensions;
using MediatR;

namespace HebrewVerb.Application.Feature.VerbModels.Commands;

public record UpdateVerbModelCommand(VerbModelDto VerbModelDto) : IRequest<Result>;

public class UpdateVerbModelCommandHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<UpdateVerbModelCommand, Result>
{
    public async Task<Result> Handle(UpdateVerbModelCommand request, CancellationToken cancellationToken)
    {
        var verbModel = _unitOfWork.VerbModelRepository.GetById(request.VerbModelDto.Id);
        if (verbModel == null)
        {
            return Result.NotFound($"Gizra with id {request.VerbModelDto.Id} doesn't exist.");
        }

        verbModel.Update(
            request.VerbModelDto.Name,
            request.VerbModelDto.Description,
            [.. request.VerbModelDto.Binyans.GetBinyans()]);
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