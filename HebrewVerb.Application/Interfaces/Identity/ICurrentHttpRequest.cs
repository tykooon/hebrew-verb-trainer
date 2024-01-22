using Ardalis.Result;

namespace HebrewVerb.Application.Interfaces.Identity;

public interface ICurrentHttpRequest<TKey> where TKey : struct
{
    Result<TKey> GetCurrentUserId();
}
