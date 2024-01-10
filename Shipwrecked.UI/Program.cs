using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Application.Services;
using Shipwrecked.Infrastructure.Interfaces;
using Shipwrecked.Infrastructure.Services;
using Shipwrecked.UI;
using Shipwrecked.UI.Interfaces;
using Shipwrecked.UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.RootComponents.Add<HeadOutlet>("head::after");

// UI services
builder.Services.AddSingleton<IDrawerService, DrawerService>();

// Infrastructure Services
builder.Services.AddScoped<IGameStore, GameStore>();
builder.Services.AddScoped<IPlayerStore, PlayerStore>();

// Application Services
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();


await builder.Build().RunAsync();

