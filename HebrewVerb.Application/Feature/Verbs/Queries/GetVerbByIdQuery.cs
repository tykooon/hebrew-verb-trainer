using Ardalis.Result;
using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;
using MediatR;

namespace HebrewVerb.Application.Feature.Verbs.Queries;

public record GetVerbByIdQuery(int VerbId) : IRequest<Result<VerbDto>>
{
}

public class GetVerbByIdQueryHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<GetVerbByIdQuery, Result<VerbDto>>
{
    public Task<Result<VerbDto>> Handle(GetVerbByIdQuery request, CancellationToken cancellationToken)
    {
        var verb = _unitOfWork.VerbRepository.GetById(request.VerbId);

        if (verb == null)
        {
            return Task.FromResult(Result<VerbDto>.NotFound($"Verb with id {request.VerbId} not found"));
        }

        var shoresh = _unitOfWork.ShoreshRepository.GetById(verb.ShoreshId);
        var present = _unitOfWork.VerbRepository.GetTenseByVerbId(verb.Id, Zman.Present) as Present;
        var past = _unitOfWork.VerbRepository.GetTenseByVerbId(verb.Id, Zman.Past) as Past;
        var future = _unitOfWork.VerbRepository.GetTenseByVerbId(verb.Id, Zman.Future) as Future;
        var imperative = _unitOfWork.VerbRepository.GetTenseByVerbId(verb.Id, Zman.Imperative) as Imperative;

        // TODO Multi Language case
        var dto = verb.ToDto(Language.Russian, shoresh, past, present, future, imperative);
        return Task.FromResult(Result.Success(dto));
    }
}
