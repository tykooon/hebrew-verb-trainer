namespace HebrewVerb.Application.Interfaces.Identity;

public interface IJwtUtils
{
    string GenerateToken(int userId, string userName, IEnumerable<string> roles);
    List<string> ValidateToken(string token);
}
