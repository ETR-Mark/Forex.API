using System;

namespace ETR.Nine.Services.Forex.API;

public interface IEndpoint
{
    void MapEndPoint(IEndpointRouteBuilder app);
}
