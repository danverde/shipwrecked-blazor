using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shipwrecked.Application.Context;
using Shipwrecked.Application.Factories;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Application.Services;
using Shipwrecked.Infrastructure;
using Shipwrecked.Infrastructure.Interfaces;
using Shipwrecked.UI;
using Shipwrecked.UI.Interfaces;
using Shipwrecked.UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.RootComponents.Add<HeadOutlet>("head::after");

// 3rd party services
builder.Services.AddBlazoredLocalStorageAsSingleton();

// UI services
builder.Services.AddScoped<IStateStorage, LocalStorageStore>();
builder.Services.AddSingleton<IDrawerService, DrawerService>();

// Infrastructure Services
builder.Services.AddSingleton<IContext, Context>();
builder.Services.AddSingleton<IReadContext>(provider => provider.GetRequiredService<IContext>());

// Application Services
builder.Services.AddSingleton<IGameSettingsFactory, GameSettingsFactory>();
builder.Services.AddSingleton<IGameContext, GameContext>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();


await builder.Build().RunAsync();

