using ETR.Nine.Services.Forex.Infrastructure.Persistence.Database;
using ETR.Nine.Services.Forex.Infrastructure.Repositories;
using ETR.Nine.Services.Forex.Infrastructure.Services.Forex;

namespace ETR.Nine.Services.Forex.Infrastructure.Services
{
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

        public async Task<ForexRate> GetForexByDate(DateTime date, string baseCurrency)
        {
            var currency = await _externalForexService.GetCurrencyRateAsync(date, baseCurrency);
            var newForexRate = new ForexRate
            {
                BaseCurrency = currency.Base,
                Rate = currency.Rates["PHP"],
                DateCreated = date
            };

            return newForexRate;
        }
    }
}