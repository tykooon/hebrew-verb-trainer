﻿using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.SharedKernel.Enums;
using MediatR;

namespace HebrewVerb.Application.Feature.Verbs.Queries;

public record GetVerbInfosByFilterQuery(Filter Filter) : IRequest<IEnumerable<VerbInfo>>;


public class GetVerbInfosByFilterQueryHandler :
    IRequestHandler<GetVerbInfosByFilterQuery, IEnumerable<VerbInfo>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetVerbInfosByFilterQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IEnumerable<VerbInfo>> Handle(GetVerbInfosByFilterQuery request, CancellationToken cancellationToken)
    {
        var binyanIds = request.Filter.Binyans.Select(str => Binyan.FromName(str).Value);
        var allVerbs = _unitOfWork.VerbRepository.GetAll();

        if (binyanIds.Any())
        {
            allVerbs = allVerbs.Where(v => binyanIds.Contains(v.Binyan.Value));
        }

        if (request.Filter.Gizras.Any())
        {
            var gizras = _unitOfWork.GizraRepository
                .FindAllBy(g => request.Filter.Gizras.Contains(g.Name));
            allVerbs = allVerbs.Where(v => v.Gizras.Except(gizras).Any());
        }

        if (request.Filter.VerbModels.Any())
        {
            var verbModels = _unitOfWork.VerbModelRepository
                .FindAllBy(g => request.Filter.VerbModels.Contains(g.Name));

            allVerbs = allVerbs.Where(v => v.VerbModels.Except(verbModels).Any());
        }

        return Task.FromResult(allVerbs.Select(v => v.ToVerbInfo()));
    }
}