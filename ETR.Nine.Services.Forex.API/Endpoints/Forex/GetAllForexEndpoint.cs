using System;
using ETR.Nine.Core.Web;
using ETR.Nine.Mediator;
using ETR.Nine.Services.Forex.Application.Forex.Queries.GetAllForex;

namespace ETR.Nine.Services.Forex.API.Endpoints.Forex;

public class GetAllForexEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/internal/forex", async (IMediator mediator) =>
        {
            var query = new GetAllForexQuery();
            var result = await mediator.Send(query);
            return Results.Ok(result);
        });
    }
}
