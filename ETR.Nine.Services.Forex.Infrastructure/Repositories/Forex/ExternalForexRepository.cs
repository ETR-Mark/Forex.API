using System;
using System.Text.Json;
using ETR.Nine.Services.Forex.Domain.Models;
using ETR.Nine.Services.Forex.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Serializers.Json;

namespace ETR.Nine.Services.Forex.Infrastructure.Repositories.Forex;
public interface IExternalForexRepository
{
    Task<Result<ExternalForexResponseModel>> GetCurrencyRateAsync(DateTime targetDate, string baseCurrency, string targetCurrency);
    Task<Result<ExternalForexResponseModel>> GetCurrencyRateTodayAsync(string baseCurrency, string targetCurrency);
}

public class ExternalForexRepository : IExternalForexRepository
{
    private readonly ExternalForexAPISettings _settings;
    private readonly RestClient _client;
    public ExternalForexRepository(IOptions<ExternalForexAPISettings> settings)
    {
        _settings = settings.Value;
        _client = new RestClient(_settings.BaseUrl, configureSerialization: s =>
        {
            s.UseSystemTextJson(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        });
    }

    private RestRequest CreateRequest(string path, string baseCurrency, string targetCurrency)
    {
        var request = new RestRequest(path, Method.Get);
        request.AddQueryParameter("api_key", _settings.ApiKey);
        request.AddQueryParameter("base", baseCurrency);
        request.AddQueryParameter("currencies", targetCurrency);
        return request;
    }

    public async Task<Result<ExternalForexResponseModel>> GetCurrencyRateAsync(DateTime targetDate,string baseCurrency, string targetCurrency)
    {
        try
        {
            var request = CreateRequest($"/{targetDate:yyyy-MM-dd}", baseCurrency, targetCurrency);
        
            var response = await _client.ExecuteAsync<ExternalForexResponseModel>(request);

            if (!response.IsSuccessful)
            {
                var msg = response.ErrorMessage 
                          ?? response.ErrorException?.Message 
                          ?? "Unknown error";

                return new Result<ExternalForexResponseModel>
                {
                    Successful = false,
                    Error = Error.Problem("FOREX-455", msg)
                };
            }

            if (response.Data == null)
            {
                return new Result<ExternalForexResponseModel>
                {
                    Successful = false,
                    Error = Error.Problem("FOREX-455", "Invalid Or No Response from API Request")
                };
            }

            if (!response.Data.Success && response.Data.Error != null)
                {
                    return new Result<ExternalForexResponseModel>
                    {
                        Successful = false,
                        Error = Error.Problem("FOREX-455", response.Data.Error.Message)
                    };
                }

            return new Result<ExternalForexResponseModel>
            {
                Successful = true,
                Data = response.Data
            };

        }catch (Exception ex)
        {
            return new Result<ExternalForexResponseModel>
            {
              Successful = false,
              Error = Error.Exception(ex)  
            };
        }
    }

    public async Task<Result<ExternalForexResponseModel>> GetCurrencyRateTodayAsync(string baseCurrency, string targetCurrency)
    {
        try
        {
            var request = CreateRequest("/latest", baseCurrency, targetCurrency);

            var response = await _client.ExecuteAsync<ExternalForexResponseModel>(request);

            if (!response.IsSuccessful)
            {
                var msg = response.ErrorMessage 
                          ?? response.ErrorException?.Message 
                          ?? "Unknown error";

                return new Result<ExternalForexResponseModel>
                {
                    Successful = false,
                    Error = Error.Problem("FOREX-455", msg)
                };
            }
            

            if (response.Data == null)
            {
                return new Result<ExternalForexResponseModel>
                {
                    Successful = false,
                    Error = Error.Problem("FOREX-455", "Invalid Or No Response from API Request")
                };
            }

            if (!response.Data.Success && response.Data.Error != null)
                {
                    return new Result<ExternalForexResponseModel>
                    {
                        Successful = false,
                        Error = Error.Problem("FOREX-455", response.Data.Error.Message)
                    };
                }

            return new Result<ExternalForexResponseModel>
            {
                Successful = true,
                Data = response.Data
            };
        }catch (Exception ex)
        {
            return new Result<ExternalForexResponseModel>
            {
              Successful = false,
              Error = Error.Exception(ex)  
            };
        }
    }
}
