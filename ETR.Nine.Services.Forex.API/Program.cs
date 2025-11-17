using ETR.Nine.Services.Forex.API.Endpoints;
using ETR.Nine.Services.Forex.API.Endpoints.Forex;
using ETR.Nine.Services.Forex.API.Middlewares;
using ETR.Nine.Services.Forex.Application.Forex.Commands.CreateForex;
using ETR.Nine.Services.Forex.Application.Forex.Queries.GetAllForex;
using ETR.Nine.Services.Forex.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMediatR(c =>
{
    c.RegisterServicesFromAssembly(typeof(GetAllForexQuery).Assembly);
    c.RegisterServicesFromAssembly(typeof(CreateForexCommand).Assembly);
});

var app = builder.Build();

app.UseMiddleware<ForexApiExceptionMiddleware>();

new GetAllForexEndpoint().MapEndPoint(app);
new GetForexByDateEndpoint().MapEndPoint(app);

app.Run();