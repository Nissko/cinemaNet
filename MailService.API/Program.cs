using MailService.Application.Common;
using MailService.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebAssemblyApp", policy =>
    {
        //TODO: указание Dev/Host
        policy.WithOrigins("https://aib-cinema.ru/")
        //policy.WithOrigins("http://localhost:5294")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddSwaggerGen();

builder.Services.Configure<MailSettings>(
    builder
        .Configuration
        .GetSection(nameof(MailSettings))
);
builder.Services.AddTransient<IMailService, MailService.Infrastructure.Services.MailService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();