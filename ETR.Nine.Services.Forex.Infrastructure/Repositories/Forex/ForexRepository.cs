using ETR.Nine.Services.Forex.Infrastructure.Persistence;
using ETR.Nine.Services.Forex.Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace ETR.Nine.Services.Forex.Infrastructure.Repositories
{
    public interface IForexRepository
    {
        Task<List<ForexRate>> GetAll();
        Task<ForexRate?> GetByDate(DateTime dateTime);
        Task<ForexRate> Create(ForexRate forexRate);
    }
    
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
            return await _dbContext.ForexRates.ToListAsync();
        }

        public async Task<ForexRate?> GetByDate(DateTime dateTime)
        {
            return await _dbContext.ForexRates
                                        .Where(f => f.RateDate == dateTime)
                                        .FirstOrDefaultAsync();
        }
    }
}