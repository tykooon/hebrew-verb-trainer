using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Enums;
using MediatR;

namespace HebrewVerb.Application.Feature.Verbs.Queries;

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
            allVerbs = allVerbs.Where(v => v.Shoresh.Gizras.Except(gizras).Any());
        }

        if (request.Filter.VerbModels.Any())
        {
            var verbModels = _unitOfWork.VerbModelRepository
                .FindAllBy(g => request.Filter.VerbModels.Contains(g.Name));

            allVerbs = allVerbs.Where(v => v.VerbModels.Except(verbModels).Any());
        }

        return Task.FromResult(allVerbs.Select(v => new VerbInfo(v.Id, v.Infinitive.Hebrew)));
    }
}
