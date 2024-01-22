using Ardalis.Result;
using HebrewVerb.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace HebrewVerb.Infrastructure.AppUserServices;

internal class CurrentHttpRequest : ICurrentHttpRequest<int>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentHttpRequest(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Result<int> GetCurrentUserId()
    {
        var idString = _httpContextAccessor
            .HttpContext!.User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

        if (int.TryParse(idString, out var userId))
        {
            return Result.Success(userId);
        }
        return Result.Invalid(new ValidationError("Invalid current User Id"));
    }
}
