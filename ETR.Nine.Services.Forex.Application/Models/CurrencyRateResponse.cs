using System;
using System.Text.Json.Serialization;

namespace ETR.Nine.Services.Forex.Application.Models;

public class CurrencyRateResponse
{
    public bool Success { get; set; }

    public string Base { get; set; } = string.Empty;
    
    public long Timestamp { get; set; }

    // "rates": { "PHP": 58.9249845 }
    public Dictionary<string, decimal> Rates { get; set; } = new();
}
