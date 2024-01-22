
namespace HebrewVerb.Application.Models;

public class AppUserDetails
{
    public AppUserDetails(int userId, string username, string email, IEnumerable<string> roles)
    {
        UserId = userId;
        Username = username;
        Email = email;
        Roles = [.. roles];
    }

    public int UserId { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public List<string> Roles { get; set; } = [];

    public string Token { get; set; } = string.Empty;
}
