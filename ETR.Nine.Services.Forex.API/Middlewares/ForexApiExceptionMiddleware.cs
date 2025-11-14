using System;

namespace ETR.Nine.Services.Forex.API.Middlewares;

public class ForexApiException : Exception
{
    public string Code { get; }

    public ForexApiException(string code, string message) : base(message)
    {
        Code = code;
    }
}

public class ForexApiExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public ForexApiExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ForexApiException e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = 455;

            var response = new
            {
              e.Code,
              Description = e.Message  
            };

            await httpContext.Response.WriteAsJsonAsync(response);
        }
    }
}

