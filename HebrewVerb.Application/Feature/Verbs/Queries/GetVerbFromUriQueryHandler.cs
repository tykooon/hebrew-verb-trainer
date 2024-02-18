using HebrewVerb.Application.Common.Helpers;
using MediatR;

namespace HebrewVerb.Application.Feature.Verbs.Queries;

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
