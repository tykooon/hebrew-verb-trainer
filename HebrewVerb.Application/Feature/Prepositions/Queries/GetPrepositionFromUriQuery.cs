using Ardalis.Result;
using HebrewVerb.Application.Common.Helpers;
using HebrewVerb.Application.Models;
using MediatR;

namespace HebrewVerb.Application.Feature.Prepositions.Queries;

public record GetPrepositionFromUriQuery(string Url) : IRequest<Result<PrepositionDto>>;

public class GetPrepositionFromUriQueryHandler : IRequestHandler<GetPrepositionFromUriQuery, Result<PrepositionDto>>
{
    public async Task<Result<PrepositionDto>> Handle(GetPrepositionFromUriQuery request, CancellationToken cancellationToken)
    {
        var result = await PrepositionParser.FromUri(request.Url);

        return result;
    }
}

