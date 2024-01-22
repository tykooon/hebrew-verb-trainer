using Microsoft.Extensions.Configuration;
using Ardalis.GuardClauses;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HebrewVerb.Application.Interfaces.Identity;


namespace HebrewVerb.Infrastructure.Identity;

public class JwtUtils(IConfiguration configuration) : IJwtUtils
{
    private readonly IConfiguration _configuration = configuration;

    public string GenerateToken(int userId, string userName, IEnumerable<string> roles)
    {
        var jwtSettings = _configuration.GetSection("JwtOptions");
        Guard.Against.Null(jwtSettings, message: "JwtOptions not found");
        var secret = Guard.Against.NullOrEmpty(jwtSettings["Secret"], message: "'Secret' not found or empty.");
        var issuer = Guard.Against.NullOrEmpty(jwtSettings["Issuer"], message: "'Issuer' not found or empty.");
        var audience = Guard.Against.NullOrEmpty(jwtSettings["Audience"], message: "'Audience' not found or empty.");
        var expiryInMinutes = Guard.Against.NullOrEmpty(jwtSettings["ExpiryInMinutes"], message: "'ExpiryInMinutes' not found or empty.");
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>()
        {
            new(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, userName),
            new(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, userId.ToString()),
            new("UserId", userId.ToString())
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(expiryInMinutes)),
            signingCredentials: signingCredentials);
        var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return encodedToken;
    }

    public List<string> ValidateToken(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return [];
        }
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSettings = _configuration.GetSection("JwtOptions");
        Guard.Against.Null(jwtSettings, message: "JwtOptions not found");
        var secret = Guard.Against.NullOrEmpty(jwtSettings["Secret"], message: "'Secret' not found or empty.");
        tokenHandler.ValidateToken(token, new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);
        var jwtToken = (JwtSecurityToken)validatedToken;
        var roles = new List<string>();

        if (jwtToken != null)
        {
            foreach (var claim in jwtToken.Claims)
            {
                if (claim.Type.ToLower() == "role")
                {
                    roles.Add(claim.Value);
                }
            }
        }
        return roles;
    }
}
