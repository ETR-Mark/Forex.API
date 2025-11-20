using ETR.Nine.Mediator;
using ETR.Nine.Services.Forex.API.Endpoints.Forex;
using ETR.Nine.Services.Forex.API.Middlewares;
using ETR.Nine.Services.Forex.Application;
using ETR.Nine.Services.Forex.Application.Forex.Queries.GetAllForex;
using ETR.Nine.Services.Forex.Infrastructure;
using ETR.Nine.Services.Forex.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddMediator(
    typeof(GetAllForexQuery).Assembly
);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ForexDbContext>();
db.Database.EnsureCreated();

new GetAllForexEndpoint().Map(app);
new GetForexByDateEndpoint().Map(app);

app.Run();