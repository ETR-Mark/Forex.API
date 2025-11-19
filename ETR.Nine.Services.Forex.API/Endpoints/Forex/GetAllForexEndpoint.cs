using System;
using ETR.Nine.Core.Web;
using ETR.Nine.Services.Forex.Application.Forex.Queries.GetAllForex;

namespace ETR.Nine.Services.Forex.API.Endpoints.Forex;

public class GetAllForexEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/internal/forex", async (int? pageIndex, int? itemsPerPage, IGetAllForexHandler handler) =>
        {
            
            var query = new GetAllForexQuery
            {
                PageIndex = pageIndex ?? 1,
                ItemsPerPage = itemsPerPage ?? 10
            };

            var result = await handler.Handle(query);
            return result.Successful ? Results.Ok(result) : Results.BadRequest(result);
        });
    }
}
