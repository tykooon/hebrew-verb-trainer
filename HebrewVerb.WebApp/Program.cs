using HebrewVerb.Application;
using HebrewVerb.Application.Entities;
using HebrewVerb.Infrastructure;
using HebrewVerb.WebApp;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddLogging();

builder.Services.AddControllers();

builder.Services.AddApplicationServices();

builder.Services.AddInfrastructureServices(configuration);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddIdentityCore<AppUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

//builder.Services.AddAuthorizationWithPolicies(configuration);

//builder.Services.AddJwtTokenOptions(configuration);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityApi<AppUser>();
app.MapControllers();


app.Run();
