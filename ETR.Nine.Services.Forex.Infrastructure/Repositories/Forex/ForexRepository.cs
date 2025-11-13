using ETR.Nine.Services.Forex.Infrastructure.Persistence;
using ETR.Nine.Services.Forex.Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace ETR.Nine.Services.Forex.Infrastructure.Repositories
{
    public class ForexRepository : IForexRepository
    {
        private readonly IAppDbContext _dbContext;
        public ForexRepository(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ForexRate> Create(ForexRate forexRate)
        {
            _dbContext.ForexRates.Add(forexRate);
            await _dbContext.SaveChangesAsync();
            return forexRate;
        }

        public async Task<List<ForexRate>> GetAll()
        {
            var forexRates = await _dbContext.ForexRates.ToListAsync();
            return forexRates;
        }

        public async Task<ForexRate?> GetByDate(DateTime dateTime)
        {
            var forexRate = await _dbContext.ForexRates
                                        .Where(f => f.RateDate == dateTime)
                                        .FirstOrDefaultAsync();
            return forexRate;
        }
    }
}