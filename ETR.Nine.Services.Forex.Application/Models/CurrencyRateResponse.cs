using System;
using System.Text.Json.Serialization;

namespace ETR.Nine.Services.Forex.Application.Models;

public class CurrencyRateResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    
    [JsonPropertyName("base")]
    public string Base { get; set; } = string.Empty;
    
    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }

    // "rates": { "PHP": 58.9249845 }
    [JsonPropertyName("rates")]
    public Dictionary<string, decimal> Rates { get; set; } = new();
}
