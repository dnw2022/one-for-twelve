using Dnw.OneForTwelve.Core.Extensions;
using Dnw.OneForTwelve.Core.models;
using Dnw.OneForTwelve.Core.services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

// Add services to the container.

// Cache in the init phase of the lambda because more cpu is available and it's free
builder.Services.AddGameServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    var architecture = System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture;
    var dotnetVersion = Environment.Version.ToString();
    return $"Architecture: {architecture}, .NET Version: {dotnetVersion}";
});

app.MapGet("/games/{language}/{questionSelectionStrategy}", (Languages language, QuestionSelectionStrategies questionSelectionStrategy, IGameService gameService) => {
    var game = gameService.Start(language, questionSelectionStrategy);
    return game == null ? Results.BadRequest() : Results.Ok(game);
});

app.Run();