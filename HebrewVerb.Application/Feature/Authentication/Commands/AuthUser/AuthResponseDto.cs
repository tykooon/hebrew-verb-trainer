namespace HebrewVerb.Application.Feature.Authentication.Commands.AuthUser;

public class AuthResponseDto
{
    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public IList<string> Roles { get; set; } = new List<string>();

    public string? Token { get; set; }
}