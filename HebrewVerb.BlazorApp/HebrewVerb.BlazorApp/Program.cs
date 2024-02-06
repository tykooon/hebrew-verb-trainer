using HebrewVerb.BlazorApp;
using HebrewVerb.BlazorApp.Components;
using MudBlazor.Services;
using HebrewVerb.Application;
using HebrewVerb.Infrastructure;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddMudServices();

var configuration = builder.Configuration;

builder.Services.AddLogging();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(configuration);
builder.Services.AddHttpContextAccessor();

//builder.Services.AddSingleton()

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseSession();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(HebrewVerb.BlazorApp.Client._Imports).Assembly);

app.Run();
