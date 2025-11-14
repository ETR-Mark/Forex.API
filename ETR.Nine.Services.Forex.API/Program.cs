using ETR.Nine.Services.Forex.API.Endpoints;
using ETR.Nine.Services.Forex.API.Middlewares;
using ETR.Nine.Services.Forex.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ForexApiExceptionMiddleware>();

new ForexEndpoint().MapEndPoint(app);

app.Run();