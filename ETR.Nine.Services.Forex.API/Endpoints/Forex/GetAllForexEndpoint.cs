using System;
using ETR.Nine.Services.Forex.Infrastructure.Services;

namespace ETR.Nine.Services.Forex.API.Endpoints.Forex;

public class GetAllForexEndpoint : IEndpoint
{
    public void MapEndPoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/internal/forex", async (IForexService forexService) =>
        {
            var result = await forexService.GetAllForex();
            if(!result.Success) return Results.BadRequest(result.Error);
            return Results.Ok(result);
        });
    }
}
