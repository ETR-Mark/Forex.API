using System;
using System.Text.Json;
using ETR.Nine.Services.Forex.Application.Models;
using ETR.Nine.Services.Forex.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace ETR.Nine.Services.Forex.Infrastructure.Repositories.Forex;

public class ExternalForexRepository : IExternalForexRepository
{
    private readonly HttpClient _httpClient;
    private readonly ExternalForexAPISettings _settings;
    public ExternalForexRepository(HttpClient httpClient, IOptions<ExternalForexAPISettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
    }

    public async Task<CurrencyRateResponse> GetCurrencyRateAsync(DateTime targetDate,string baseCurrency, string targetCurrency)
    {
        var url = $"{_settings.BaseUrl}/{targetDate:yyyy-MM-dd}?api_key={_settings.ApiKey}&base={baseCurrency}&currencies={targetCurrency}";

        var response = await _httpClient.GetAsync(url);

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<CurrencyRateResponse>(jsonResponse);

        if (result is null)
            throw new Exception("Failed to deserialize");

        return result;
    }
}
