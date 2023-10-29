using Domain.DependencyInjection;
using Domain.UseCases.ConsolidatePosition;
using Infrastructure.Database.DependencyInjection;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddDataBase(config.GetSection("connectionString").Value);
builder.Services.AddDomainDependecies();

/* App configuration */

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/movement", CreateMovement);
app.MapPost("/position", ConsolidatePostion);

static async Task<Created<AddMovementCommand>> CreateMovement(AddMovementCommand command, AddMovementUseCase useCase)
{
    await useCase.Execute(command);
    return TypedResults.Created("/movement", command);
}

static async Task<Ok> ConsolidatePostion(ConsolidatePositionUseCase useCase)
{
    await useCase.Execute(DateTime.Today);
    return TypedResults.Ok();
}

app.Run();



public partial class Program
{ }