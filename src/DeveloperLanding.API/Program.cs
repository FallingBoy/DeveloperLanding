using DeveloperLanding.API.Middleware;
using DeveloperLandingApi.Application;
using DeveloperLandingApi.Infrastructure;
using DeveloperLandingApi.Infrastructure.Options;
using FluentValidation.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().WriteTo.File("Logs/api-.log", rollingInterval: RollingInterval.Day).CreateLogger();


builder.Host.UseSerilog();
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<OpenRouterOptions>(builder.Configuration.GetSection("OpenRouter"));

builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
    {
        options.AddPolicy(
            "frontend",
            policy =>
            {
                policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
    });

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<RateLimitMiddleware>();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("frontend");
app.MapControllers();
app.Run();