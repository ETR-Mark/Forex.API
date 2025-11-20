using System;
using System.Globalization;
using ETR.Nine.Core.Web;
using ETR.Nine.Services.Forex.Application.Models;
using ETR.Nine.Services.Forex.Infrastructure.Services;

namespace ETR.Nine.Services.Forex.API.Endpoints.Forex;

public class GetForexByDateEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/internal/forex/{date}", async (string date, string? c, IForexService forexService) =>
        {
            bool isValidDate = DateTime.TryParseExact(date, "MMddyyyy", null, DateTimeStyles.None, out var parsedDate);
            if(!isValidDate) return Results.BadRequest(new
            {
                Code = "INTERNAL_ERROR",
                Description = "Invalid date format. Use DDMMYYYY."
            });

            var result = await forexService.GetForexByDate(parsedDate, c ?? "USD"); 

            if (!result.Successful)
            {
                return Results.BadRequest(new
                {
                    Code = "FOREX-455",
                    Description = result.Error?.Message ?? "Unknown error"
                });
            }
                
            var response = new SuccessResponse(c, "PHP", result.Data.Rate);
            return Results.Ok(response);
        });
    }
}
