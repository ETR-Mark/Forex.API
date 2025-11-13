using System;
using ETR.Nine.Services.Forex.Application.Models;

namespace ETR.Nine.Services.Forex.Infrastructure.Repositories.Forex;

public interface IExternalForexRepository
{
    // "rates": { "PHP": 58.9249845 }
    Task<CurrencyRateResponse> GetCurrencyRateAsync(DateTime targetDate, string baseCurrency, string targetCurrency);
}
