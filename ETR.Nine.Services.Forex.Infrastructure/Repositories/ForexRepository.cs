using ETR.Nine.Services.Forex.Application.Interfaces.Repositories;
using ETR.Nine.Services.Forex.Domain.Entity;
using ETR.Nine.Services.Forex.Infrastructure.Persistence;
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

        public async Task<ForexRate?> GetByDate(DateTime dateTime)
        {
            var forexRate = await _dbContext.ForexRates
                                        .Where(f => f.DateCreated == dateTime)
                                        .FirstOrDefaultAsync();
            return forexRate;
        }
    }
}