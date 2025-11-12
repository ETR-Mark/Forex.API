using ETR.Nine.Services.Forex.Application.Interfaces.Repositories;
using ETR.Nine.Services.Forex.Application.Interfaces.IServices;
using ETR.Nine.Services.Forex.Domain.Entity;

namespace ETR.Nine.Services.Forex.Application.Services
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

        public Task<ForexRate> GetForexByDate(DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}