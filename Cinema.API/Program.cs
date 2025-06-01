using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Cinema.Application.Application.Extensions;
using Cinema.Application.Common.Interfaces;
using Cinema.Infrastructure.Extensions;
using Cinema.Infrastructure.Services;
using Cinema.Infrastructure.Services.MainPageHub;
using Cinema.Infrastructure.Setting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.IdentityModel.Tokens;

ContainerBuilder build = new ContainerBuilder();
//build.RegisterModule(new ApplicationModule());

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/wasm" });
});

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
    });
builder.Services.AddSignalR(options => { options.EnableDetailedErrors = true; });

builder.Services
    .AddCinemaCollectionInfrastructure(builder.Configuration)
    .AddApplication();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebAssemblyApp", policy =>
    {
        //TODO: указание Dev/Host
        //policy.WithOrigins("http://aib-cinema.ru/")
        policy.WithOrigins("http://localhost:5249")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Host.UseServiceProviderFactory(new AutofacChildLifetimeScopeServiceProviderFactory(build.Build()));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseResponseCompression();

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.CacheControl = "public,max-age=31536000";
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors("AllowWebAssemblyApp");
app.UseAuthorization();

app.UseWebSockets();

// Регистрация контроллеров и хаба
//TODO: указание Dev/Host

//app.MapControllers();
app.MapControllers().WithDisplayName("/api/[controller]");
app.MapHub<MovieHub>("/movieHub");
app.MapHub<EventsMainHub>("/eventsMainHub");
app.Run();