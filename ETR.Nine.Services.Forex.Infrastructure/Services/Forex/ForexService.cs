using ETR.Nine.Services.Forex.Infrastructure.Persistence.Database;
using ETR.Nine.Services.Forex.Infrastructure.Repositories;
using ETR.Nine.Services.Forex.Infrastructure.Services.Forex;

namespace ETR.Nine.Services.Forex.Infrastructure.Services
{
    public interface IForexService
    {
        Task<List<ForexRate>> GetAllForex();
        Task<ForexRate?> GetForexByDate(DateTime date, string baseCurrency);
        Task<ForexRate> CreateForexRate(ForexRate forexRate);
    }
    
    public class ForexService : IForexService
    {
        private readonly IForexRepository _forexRepository;
        private readonly IExternalForexService _externalForexService;
        public ForexService(IForexRepository forexRepository, IExternalForexService externalForexService)
        {
            _forexRepository = forexRepository;
            _externalForexService = externalForexService;
        }

        public async Task<ForexRate> CreateForexRate(ForexRate forexRate)
        {
            var newForexRate = await _forexRepository.Create(forexRate);
            return newForexRate;
        }

        public async Task<List<ForexRate>> GetAllForex()
        {
            var forexRates = await _forexRepository.GetAll();
            return forexRates;
        }

        public async Task<ForexRate?> GetForexByDate(DateTime date, string baseCurrency)
        {
            var internalForexRate = await _forexRepository.GetByDate(date);
            if(internalForexRate == null)
            {
                DateTime utcNow = DateTime.UtcNow;
                if(date.Date == utcNow.Date)
                {
                    var currencyToday = await _externalForexService.GetCurrencyRateTodayAsync(baseCurrency);

                    if (currencyToday.Rates.ContainsKey("PHP"))
                    {
                        return await _forexRepository.Create(new ForexRate
                        {
                            BaseCurrency = baseCurrency,
                            Rate = currencyToday.Rates["PHP"],
                            RateDate = utcNow.Date
                        });
                    }
                }
                var currency = await _externalForexService.GetCurrencyRateAsync(date, baseCurrency);

                if (currency.Rates.ContainsKey("PHP"))
                {
                    return await _forexRepository.Create(new ForexRate
                    {
                        BaseCurrency = baseCurrency,
                        Rate = currency.Rates["PHP"],
                        RateDate = DateTimeOffset.FromUnixTimeSeconds(currency.Timestamp).UtcDateTime.Date
                    });
                }
            }

            return internalForexRate;
        }
    }
}

