using System;
using System.Data.Common;
using System.Globalization;
using ETR.Nine.Services.Forex.Infrastructure;
using ETR.Nine.Services.Forex.Infrastructure.Services;
using ETR.Nine.Services.Forex.Infrastructure.Services.Forex;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddHttpClient();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

var forexGroup = app.MapGroup("internal/forex");

forexGroup.MapGet("/", async (IForexService forexService) =>
{
    var forexRates = await forexService.GetAllForex();
    return forexRates;
});

forexGroup.MapGet("/{date}", async (string date, string? c, IForexService forexService) =>
{
    if (DateTime.TryParseExact(date, "MMddyyyy", null, DateTimeStyles.None, out var parsedDate))
    {
        string dateToday = DateTime.UtcNow.ToString("MM-dd-yyyy");
        // if (parsedDate.Date == DateTime.UtcNow.Date)
        // {
        //     return Results.Ok($"Forex date output: {parsedDate:MM-dd-yyyy} (Today) ||| Today is {dateToday} FROM: {c}");
        // }
        // else
        // {
        //     return Results.Ok($"Forex date output: {parsedDate:MM-dd-yyyy} (Other date) ||| Today is {dateToday} FROM: {c}");
        // }
        var currency = await forexService.GetForexByDate(parsedDate, c);
        return Results.Ok(currency);
    }
    else
    {
        return Results.BadRequest("Invalid date format. Use DDMMYYYY.");
    }
});


app.Run();