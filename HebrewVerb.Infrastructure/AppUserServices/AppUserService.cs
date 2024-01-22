using HebrewVerb.Application.Entities;
using HebrewVerb.Application.Interfaces.Identity;
using HebrewVerb.Application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Infrastructure.AppUserServices;

public class AppUserService : IAppUserService
{
    private readonly UserManager<AppUser> _userManager;

    public AppUserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<AppUserDetails> GetUserDetailsByEmailAsync(string email)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email)
            ?? throw new Exception("User not found");
        var roles = await _userManager.GetRolesAsync(user);

        return new AppUserDetails(user.Id!, user.UserName!, user.Email!, roles);
    }

    public async Task<AppUserDetails> GetUserDetailsByUserIdAsync(int userId)
    {
        var user = await _userManager.Users.Include(u => u.FilterSnapshots).FirstOrDefaultAsync(x => x.Id == userId)
            ?? throw new Exception("User not found");
        var roles = await _userManager.GetRolesAsync(user);

        return new AppUserDetails(user.Id!, user.UserName!, user.Email!, roles);
    }

    public async Task<AppUserDetails> GetUserDetailsByUserNameAsync(string userName)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName)
            ?? throw new Exception("User not found");
        var roles = await _userManager.GetRolesAsync(user);

        return new AppUserDetails(user.Id!, user.UserName!, user.Email!, roles);
    }

    public async Task<string?> GetUserNameAsync(int userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }
}

