using System;
using ETR.Nine.Services.Forex.Application.Exceptions;

namespace ETR.Nine.Services.Forex.API.Middlewares;

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

        // For Forex
        catch (ForexApiException fe)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = 400; // Bad API request

            var response = new
            {
              Code = fe.Code,
              Description = fe.Message  
            };

            await httpContext.Response.WriteAsJsonAsync(response);
        }

        // For my internal
        catch (Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = 500; // Server Error

            var response = new
            {
              Code = $"INTERNAL_ERROR",
              Description = ex.Message  
            };

            await httpContext.Response.WriteAsJsonAsync(response);
        }
    }
}

