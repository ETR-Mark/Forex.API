using System;
using ETR.Nine.Services.Forex.Infrastructure;
using ETR.Nine.Services.Forex.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () =>
{
    return "Hello World!";
});

var forexGroup = app.MapGroup("internal/forex");

forexGroup.MapGet("/", async (IForexService forexService) =>
{
    var forexRates = await forexService.GetAllForex();
    return forexRates;
});

forexGroup.MapGet("/{date}", (string date) =>
{
    if (DateTime.TryParseExact(date, "MMddyyyy", null, System.Globalization.DateTimeStyles.None, out var parsedDate))
    {
        string dateToday = DateTime.UtcNow.ToString("MM-dd-yyyy");
        if (parsedDate.Date == DateTime.UtcNow.Date)
        {
            return Results.Ok($"Forex date output: {parsedDate:MM-dd-yyyy} (Today) ||| Today is {dateToday}");
        }
        else
        {
            return Results.Ok($"Forex date output: {parsedDate:MM-dd-yyyy} (Other date) ||| Today is {dateToday}");
        }
    }
    else
    {
        return Results.BadRequest("Invalid date format. Use DDMMYYYY.");
    }
});


app.Run();