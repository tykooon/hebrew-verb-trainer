using HebrewVerb.Application.Common.Helpers;
using MediatR;

namespace HebrewVerb.Application.Feature.Verbs.Queries;

public class GetVerbFromUriQueryHandler :
    IRequestHandler<GetVerbFromUriQuery, VerbDto>
{
    public Task<VerbDto> Handle(GetVerbFromUriQuery request, CancellationToken cancellationToken)
    {
        var result = VerbParser.FromUri(request.url, request.passive);

        if (!result.IsSuccess)
        {
            return Task.FromResult(new VerbDto());
        }

        return Task.FromResult(result.Value);
    }
}
