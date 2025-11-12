using ETR.Nine.Services.Forex.Infrastructure.Persistence.Database;
using ETR.Nine.Services.Forex.Infrastructure.Repositories;

namespace ETR.Nine.Services.Forex.Infrastructure.Services
{
    public class ForexService : IForexService
    {
        private readonly IForexRepository _forexRepository;
        public ForexService(IForexRepository forexRepository)
        {
            _forexRepository = forexRepository;
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

        public Task<ForexRate> GetForexByDate(DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}