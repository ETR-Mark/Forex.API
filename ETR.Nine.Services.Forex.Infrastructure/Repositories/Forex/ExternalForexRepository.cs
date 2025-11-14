using System;
using System.Text.Json;
using ETR.Nine.Services.Forex.Application.Models;
using ETR.Nine.Services.Forex.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using RestSharp;

namespace ETR.Nine.Services.Forex.Infrastructure.Repositories.Forex;
public interface IExternalForexRepository
{
    Task<CurrencyRateResponse> GetCurrencyRateAsync(DateTime targetDate, string baseCurrency, string targetCurrency);
    Task<CurrencyRateResponse> GetCurrencyRateTodayAsync(string baseCurrency, string targetCurrency);
}

public class ExternalForexRepository : IExternalForexRepository
{
    private readonly ExternalForexAPISettings _settings;
    public ExternalForexRepository(IOptions<ExternalForexAPISettings> settings)
    {
        _settings = settings.Value;
    }

    private RestClient Client => new RestClient($"{_settings.BaseUrl}");
    private RestRequest CreateRequest(string path, string baseCurrency, string targetCurrency)
    {
        var request = new RestRequest(path, Method.Get);
        request.AddQueryParameter("api_key", _settings.ApiKey);
        request.AddQueryParameter("base", baseCurrency);
        request.AddQueryParameter("currencies", targetCurrency);
        return request;
    }

    public async Task<CurrencyRateResponse> GetCurrencyRateAsync(DateTime targetDate,string baseCurrency, string targetCurrency)
    {
        var request = CreateRequest($"/{targetDate:yyyy-MM-dd}", baseCurrency, targetCurrency);

        var response = await Client.ExecuteAsync<CurrencyRateResponse>(request);

        if(response.Data == null)
        {
            throw new Exception("Failed to deserialize");
        } 

        return response.Data;
    }

    public async Task<CurrencyRateResponse> GetCurrencyRateTodayAsync(string baseCurrency, string targetCurrency)
    {
        var request = CreateRequest("/latest", baseCurrency, targetCurrency);
        var response = await Client.ExecuteAsync<CurrencyRateResponse>(request);

        if(response.Data == null)
        {
            throw new Exception("Failed to deserialize");
        }

        return response.Data;
    }
}
