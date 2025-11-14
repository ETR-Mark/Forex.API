using System;
using System.Globalization;
using ETR.Nine.Services.Forex.Application.Models;
using ETR.Nine.Services.Forex.Infrastructure.Services;

namespace ETR.Nine.Services.Forex.API.Endpoints;

public class ForexEndpoint : IEndpoint
{
    public void MapEndPoint(IEndpointRouteBuilder app)
    {
        var forexGroup = app.MapGroup("/internal/forex");

        forexGroup.MapGet("/", async (IForexService forexService) =>
        {
            var forexRates = await forexService.GetAllForex();
            return forexRates;
        });

        forexGroup.MapGet("/{date}", async (string date, string? c, IForexService forexService) =>
        {
            bool isValidDate = DateTime.TryParseExact(date, "MMddyyyy", null, DateTimeStyles.None, out var parsedDate);
            if(!isValidDate) return Results.BadRequest("Invalid date format. Use DDMMYYYY.");

            var forex = await forexService.GetForexByDate(parsedDate, c);
            var response = new SuccessResponse(c, "PHP", forex.Rate);
            return Results.Ok(response);    
        });
        
    }
}
