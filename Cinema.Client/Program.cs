using Cinema.Client;
using Cinema.Client.Services;
using Cinema.Client.Services.JwtService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//TODO: указание Dev/Host

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://aib-cinema.ru/api/") });
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5294/api") });
builder.Services.AddScoped<AuthService>();
builder.Services.AddSingleton<JwtService>();

await builder.Build().RunAsync();