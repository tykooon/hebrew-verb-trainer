using Ardalis.GuardClauses;
using HebrewVerb.Application.Entities;
using HebrewVerb.Application.Interfaces.Identity;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using HebrewVerb.Infrastructure.AppUserServices;
using HebrewVerb.WebApp.Models;

namespace HebrewVerb.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //var assembly = typeof(DependencyInjection).Assembly;
        //var connectionString = configuration.GetConnectionString("MySqlConnection");
        //var connectionData = configuration
        //    .GetSection("HebrewVerb")
        //    .GetSection("Connections")
        //    .GetSection("MySql")
        //    .GetSection("Admin")
        //    .Get<MySqlConnectionSettings>();
        //connectionString = connectionData?.UpdateConnectionString(connectionString);

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");
        
        services.AddScoped<IAppDbContext, AppDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddDbContext<AppDbContext>(options =>
        {
            //options.UseMySQL(connectionString);

            options.UseSqlite(
                connectionString
                //,b => b.MigrationsAssembly("SimpleAuthClean.WebApp"
                );
        });

        services.AddIdentity<AppUser, IdentityRole<int>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.User.RequireUniqueEmail = true;
        });

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IAppUserService, AppUserService>();
        services.AddScoped<IJwtUtils, JwtUtils>();
        services.AddScoped<ICurrentHttpRequest<int>, CurrentHttpRequest>();

        return services;
    }
}
