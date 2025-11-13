using System;
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
        DateTime utcNow = DateTime.UtcNow;
        string formattedDate = parsedDate.ToString("yyyy-MM-dd");
        string dateToday = utcNow.ToString("yyyy-MM-dd");

        var currency = await forexService.GetForexByDate(parsedDate, c);
        return Results.Ok(currency);

        
        // if (parsedDate.Date == utcNow.Date)
        // {
        //     return Results.Ok($"Date Provided: before:{parsedDate} after: {formattedDate} ||| before: {utcNow} after: {dateToday}");
        // }
        // else
        // {
        //     return Results.Ok($"Date Provided: before:{parsedDate} after: {formattedDate} ||| before: {utcNow} after: {dateToday}");
        // }
    }
    else
    {
        return Results.BadRequest("Invalid date format. Use DDMMYYYY.");
    }
});


app.Run();