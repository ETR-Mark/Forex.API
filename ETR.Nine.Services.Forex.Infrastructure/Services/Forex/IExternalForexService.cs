using System;
using ETR.Nine.Services.Forex.Application.Models;

namespace ETR.Nine.Services.Forex.Infrastructure.Services.Forex;

public interface IExternalForexService
{
    Task<CurrencyRateResponse> GetCurrencyRateAsync(DateTime targetDate,string baseCurrency);
}
