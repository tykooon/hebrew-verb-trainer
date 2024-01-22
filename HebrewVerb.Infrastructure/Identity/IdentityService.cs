using Ardalis.Result;
using HebrewVerb.Application.Entities;
using HebrewVerb.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Identity;
using HebrewVerb.Application.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using HebrewVerb.Application.Common.Constants;

namespace HebrewVerb.Infrastructure.Identity;

public class IdentityService(
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole<int>> roleManager,
    SignInManager<AppUser> signInManager) : IIdentityService
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager = roleManager;
    private readonly SignInManager<AppUser> _signInManager = signInManager;

    public async Task<Result<int>> CreateUserAsync(
        string username, string email, string password)
    {
        var user = new AppUser
        {
            UserName = username,
            Email = email
        };
        var result = await _userManager.CreateAsync(user, password);
        return result.ToApplicationResultInt(user.Id);
    }

    public async Task<bool> UserExists(string email)
    {
        return await _userManager.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> IsInRoleAsync(int userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthenticateAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return false;
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, true, false);
        return result.Succeeded;
    }

    public async Task<Result> AddToRolesAsync(int userId, IEnumerable<string> roles)
    {
        var administratorRole = new IdentityRole<int>(Roles.Administrator);
        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);
        if (user == null)
        {
            return Result.Error(["User not found"]);
        }

        foreach (var role in roles)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole<int>(role));
            }
        }
        var result = await _userManager.AddToRolesAsync(user, roles);
        return result.ToApplicationResult();
    }
}

