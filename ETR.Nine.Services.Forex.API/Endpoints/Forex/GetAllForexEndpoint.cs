using System;
using ETR.Nine.Services.Forex.Application.Forex.Queries.GetAllForex;
using ETR.Nine.Services.Forex.Infrastructure.Exceptions;
using ETR.Nine.Services.Forex.Infrastructure.Services;
using MediatR;

namespace ETR.Nine.Services.Forex.API.Endpoints.Forex;

public class GetAllForexEndpoint : IEndpoint
{
    public void MapEndPoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/internal/forex", async (IMediator mediator) =>
        {
            var query = new GetAllForexQuery();
            var result = await mediator.Send(query);
            if(!result.Success) throw new ForexApiException("FOREX-455", result.Error ?? "API ERROR");
            return Results.Ok(result);
        });
    }
}
