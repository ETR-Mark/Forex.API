using System;
using ETR.Nine.Services.Forex.Application.Models;

namespace ETR.Nine.Services.Forex.Infrastructure.Repositories.Forex;

public interface IExternalForexRepository
{
    Task<CurrencyRateResponse> GetCurrencyRateAsync(DateTime targetDate, string baseCurrency, string targetCurrency);
}
