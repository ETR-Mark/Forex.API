using System;
using System.Text.Json.Serialization;

namespace ETR.Nine.Services.Forex.Application.Models;

public class CurrencyRateResponse
{
    public bool Success { get; set; }

    public string Base { get; set; } = string.Empty;
    
    public long Timestamp { get; set; }

    public Dictionary<string, decimal> Rates { get; set; } = new();

    public ForexAPIError Error {get; set;}
}

public class ForexAPIError
{
    public int StatusCode { get; set; } 
    public string Message { get; set; } = string.Empty;
}
