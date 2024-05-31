using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shipwrecked.Application.Factories;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Application.Services;
using Shipwrecked.Domain.Interfaces;
using Shipwrecked.Domain.Managers;
using Shipwrecked.Infrastructure.Interfaces;
using Shipwrecked.Infrastructure.Services;
using Shipwrecked.UI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.RootComponents.Add<HeadOutlet>("head::after");

// 3rd party services
builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(typeof(Program).Assembly);
    options.UseReduxDevTools(); // Don't want to run this on the release version
}); 

// Infrastructure Services
builder.Services.AddScoped<IAppStateStore, AppStateStore>();

// Application Services
builder.Services.AddScoped<IAppStateService, AppStateService>();
builder.Services.AddSingleton<IGameSettingsFactory, GameSettingsFactory>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();

// Domain Services
builder.Services.AddScoped<IPlayerManager, PlayerManager>();

await builder.Build().RunAsync();

