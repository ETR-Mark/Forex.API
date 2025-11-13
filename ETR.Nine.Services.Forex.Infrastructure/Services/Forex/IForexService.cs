using ETR.Nine.Services.Forex.Application.Models;
using ETR.Nine.Services.Forex.Infrastructure.Persistence.Database;

namespace ETR.Nine.Services.Forex.Infrastructure.Services
{
    public interface IForexService
    {
        Task<List<ForexRate>> GetAllForex();
        Task<CurrencyRateResponse> GetForexByDate(DateTime date, string baseCurrency);
        Task<ForexRate> CreateForexRate(ForexRate forexRate);
    }
}