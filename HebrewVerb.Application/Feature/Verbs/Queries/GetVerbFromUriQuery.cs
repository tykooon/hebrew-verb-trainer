using HebrewVerb.Application.Common.Helpers;
using HebrewVerb.Application.Models;
using MediatR;

namespace HebrewVerb.Application.Feature.Verbs.Queries;

public record GetVerbFromUriQuery(string url,bool passive = false) : IRequest<VerbDto>
{
}

public class GetVerbFromUriQueryHandler :
    IRequestHandler<GetVerbFromUriQuery, VerbDto>
{
    public async Task<VerbDto> Handle(GetVerbFromUriQuery request, CancellationToken cancellationToken)
    {
        var result = await VerbParser.FromUri(request.url, request.passive);

        if (!result.IsSuccess)
        {
            return new VerbDto();
        }

        return result.Value;
    }
}
