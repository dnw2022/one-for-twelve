using Dnw.OneForTwelve.Core.Extensions;
using Dnw.OneForTwelve.Core.Models;
using Dnw.OneForTwelve.Core.Services;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFirebaseAuth();

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

// Add services to the container.

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ConfigureDefaults();
});

// builder.Services.ConfigureJsonSerializerOptions();

// Cache in the init phase of the lambda because more cpu is available and it's free
builder.Services.AddGameServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", (IHttpContextAccessor ctx) =>
{
    var userName = ctx.HttpContext?.User.Identity?.Name ?? "anonymous";
    var architecture = System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture;
    var dotnetVersion = Environment.Version.ToString();
    return $"Username: {userName} , Architecture: {architecture}, .NET Version: {dotnetVersion}";
}).RequireAuthorization();

app.MapGet("/games/{language}/{questionSelectionStrategy}", 
    (
        IHttpContextAccessor ctx,
        ILogger<Program> logger,
        Languages language, 
        QuestionSelectionStrategies questionSelectionStrategy, 
        IGameService gameService) =>
{
    if (ctx.HttpContext!.Request.Headers.TryGetValue("Authorization", out var authHeader))
    {
        logger.LogInformation(authHeader);
    }
    
    var game = gameService.Start(language, questionSelectionStrategy);
    return game == null ? Results.BadRequest() : Results.Ok(game);
}).RequireAuthorization();

app.Run();