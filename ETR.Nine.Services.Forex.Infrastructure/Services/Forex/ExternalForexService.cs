using System;
using ETR.Nine.Services.Forex.Application.Models;
using ETR.Nine.Services.Forex.Infrastructure.Repositories.Forex;

namespace ETR.Nine.Services.Forex.Infrastructure.Services.Forex;

public interface IExternalForexService
{
    Task<CurrencyRateResponse> GetCurrencyRateAsync(DateTime targetDate,string baseCurrency);
}

public class ExternalForexService : IExternalForexService
{
    private readonly IExternalForexRepository _externalForexRepository;
    
    public ExternalForexService(IExternalForexRepository externalForexRepository)
    {
        _externalForexRepository = externalForexRepository;
    }

    public async Task<CurrencyRateResponse> GetCurrencyRateAsync(DateTime targetDate, string baseCurrency)
    {
        var currency = await _externalForexRepository.GetCurrencyRateAsync(targetDate, baseCurrency, "PHP");
        return currency;
    }
}
