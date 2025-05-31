using Cinema.Client;
using Cinema.Client.Services;
using Cinema.Client.Services.JwtService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7242") });
builder.Services.AddScoped<AuthService>();
builder.Services.AddSingleton<JwtService>();

await builder.Build().RunAsync();