using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Application.Services;
using Shipwrecked.Domain.Interfaces;
using Shipwrecked.Domain.Managers;
using Shipwrecked.Infrastructure.Interfaces;
using Shipwrecked.Infrastructure.Store;
using Shipwrecked.UI;
using Shipwrecked.UI.Services;

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

// Domain Services
builder.Services.AddScoped<IPlayerManager, PlayerManager>();

// Infrastructure Services
builder.Services.AddScoped<IAppStateStore, AppStateStore>();

// Application Services
builder.Services.AddScoped<IAppStateService, AppStateService>();
builder.Services.AddSingleton<ISettingsService, SettingsService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();

// UI Services
builder.Services.AddSingleton<IAlertService, AlertService>();

await builder.Build().RunAsync();

