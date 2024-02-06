using HebrewVerb.Application;
using HebrewVerb.Infrastructure;
using HebrewVerb.WebApp;
using HebrewVerb.WebApp.Components;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var configuration = builder.Configuration;

builder.Services.AddLogging();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();


builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(configuration);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddJwtTokenOptions(configuration);
builder.Services.AddAuthorizationWithPolicies(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Configure the HTTP request pipeline.
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

app.UseAntiforgery();

app.MapRazorPages();
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
