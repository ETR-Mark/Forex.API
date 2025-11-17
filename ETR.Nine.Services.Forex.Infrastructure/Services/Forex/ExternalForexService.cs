using System;
using ETR.Nine.Services.Forex.Domain.Models;
using ETR.Nine.Services.Forex.Infrastructure.Repositories.Forex;

namespace ETR.Nine.Services.Forex.Infrastructure.Services.Forex;

public interface IExternalForexService
{
    Task<ExternalForexResponseModel> GetCurrencyRateAsync(DateTime targetDate,string baseCurrency);
    Task<ExternalForexResponseModel> GetCurrencyRateTodayAsync(string baseCurrency);
}

public class ExternalForexService : IExternalForexService
{
    private readonly IExternalForexRepository _externalForexRepository;
    
    public ExternalForexService(IExternalForexRepository externalForexRepository)
    {
        _externalForexRepository = externalForexRepository;
    }

    public async Task<ExternalForexResponseModel> GetCurrencyRateAsync(DateTime targetDate, string baseCurrency)
    {
        var currency = await _externalForexRepository.GetCurrencyRateAsync(targetDate, baseCurrency, "PHP");
        return currency;
    }

    public async Task<ExternalForexResponseModel> GetCurrencyRateTodayAsync(string baseCurrency)
    {
        var currencyToday = await _externalForexRepository.GetCurrencyRateTodayAsync(baseCurrency, "PHP");
        return currencyToday;
    }
}
