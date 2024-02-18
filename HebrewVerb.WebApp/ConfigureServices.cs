using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HebrewVerb.WebApp;

public static class DependencyInjection
{
    public static IServiceCollection AddJwtTokenOptions(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Jwt Token Options
        var jwtSettings = configuration.GetSection("JwtOptions");
        var secret = Guard.Against.NullOrEmpty(jwtSettings.GetValue<string>("Secret"));
        var issuer = Guard.Against.NullOrEmpty(jwtSettings.GetValue<string>("Issuer"));
        var audience = Guard.Against.NullOrEmpty(jwtSettings.GetValue<string>("Audience"));
        var secretKey = Encoding.ASCII.GetBytes(secret);
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience
            };
        });
        return services;
    }

    public static IServiceCollection AddAuthorizationWithPolicies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("Default", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build())
            .AddPolicy("Administrator", new AuthorizationPolicyBuilder()
                .RequireRole("Administrator")
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build());

        return services;
    }
}
