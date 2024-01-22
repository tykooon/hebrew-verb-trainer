using Ardalis.Result;
using Microsoft.AspNetCore.Identity;

namespace HebrewVerb.Application.Common.Extensions;

public static class IdentityHelpers
{
    public static Result<int> ToApplicationResultInt(this IdentityResult result, int id)
    {
        return result.Succeeded ?
            Result.Success(id) :
            Result.Error(result.Errors.Select(e => $"Error with code {e.Code}: {e.Description}").ToArray());
    }

    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded ?
            Result.Success() :
            Result.Error(result.Errors.Select(e => $"Error with code {e.Code}: {e.Description}").ToArray());
    }

}

