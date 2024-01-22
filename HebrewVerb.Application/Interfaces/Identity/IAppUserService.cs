using HebrewVerb.Application.Models;

namespace HebrewVerb.Application.Interfaces.Identity;

public interface IAppUserService
{
    Task<string?> GetUserNameAsync(int userId);

    Task<AppUserDetails> GetUserDetailsByUserIdAsync(int userId);

    Task<AppUserDetails> GetUserDetailsByUserNameAsync(string userName);

    Task<AppUserDetails> GetUserDetailsByEmailAsync(string email);
}