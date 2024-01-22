using Ardalis.Result;

namespace HebrewVerb.Application.Interfaces.Identity;

public interface IIdentityService
{
    Task<Result> AddToRolesAsync(int userId, IEnumerable<string> roles);
    Task<bool> IsInRoleAsync(int userId, string role);
    Task<bool> AuthenticateAsync(string username, string password);
    Task<Result<int>> CreateUserAsync(string username, string email, string password);
}