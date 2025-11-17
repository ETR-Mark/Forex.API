using System;
using System.Text.Json;
using ETR.Nine.Services.Forex.Domain.Models;
using ETR.Nine.Services.Forex.Infrastructure.Exceptions;
using ETR.Nine.Services.Forex.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Serializers.Json;

namespace ETR.Nine.Services.Forex.Infrastructure.Repositories.Forex;
public interface IExternalForexRepository
{
    Task<ExternalForexResponseModel> GetCurrencyRateAsync(DateTime targetDate, string baseCurrency, string targetCurrency);
    Task<ExternalForexResponseModel> GetCurrencyRateTodayAsync(string baseCurrency, string targetCurrency);
}

public class ExternalForexRepository : IExternalForexRepository
{
    private readonly ExternalForexAPISettings _settings;
    public ExternalForexRepository(IOptions<ExternalForexAPISettings> settings)
    {
        _settings = settings.Value;
    }

    private RestClient Client => new RestClient($"{_settings.BaseUrl}", configureSerialization: serialization =>
    {
        serialization.UseSystemTextJson(new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    });

    private RestRequest CreateRequest(string path, string baseCurrency, string targetCurrency)
    {
        var request = new RestRequest(path, Method.Get);
        request.AddQueryParameter("api_key", _settings.ApiKey);
        request.AddQueryParameter("base", baseCurrency);
        request.AddQueryParameter("currencies", targetCurrency);
        return request;
    }

    public async Task<ExternalForexResponseModel> GetCurrencyRateAsync(DateTime targetDate,string baseCurrency, string targetCurrency)
    {
        var request = CreateRequest($"/{targetDate:yyyy-MM-dd}", baseCurrency, targetCurrency);
        
        var response = await Client.ExecuteAsync<ExternalForexResponseModel>(request);

        if(response.Data == null) throw new ForexApiException("FOREX-455", "Failed to deserialize");

        if (response.Data.Success == false)
        {
            throw new ForexApiException($"FOREX-455", response.Data.Error?.Message ?? "External Forex Api Error");
        }
       
        return response.Data;
    }

    public async Task<ExternalForexResponseModel> GetCurrencyRateTodayAsync(string baseCurrency, string targetCurrency)
    {
        var request = CreateRequest("/latest", baseCurrency, targetCurrency);

        var response = await Client.ExecuteAsync<ExternalForexResponseModel>(request);

        if(response.Data == null) throw new ForexApiException($"FOREX-455", "Failed to deserialize");

        if (response.Data.Success == false)
        {
            throw new ForexApiException($"FOREX-455", response.Data.Error.Message ?? "External Forex Api Error");
        }

        return response.Data;
    }
}
