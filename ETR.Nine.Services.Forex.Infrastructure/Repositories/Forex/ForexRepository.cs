using ETR.Nine.Services.Forex.Infrastructure.Persistence;
using ETR.Nine.Services.Forex.Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace ETR.Nine.Services.Forex.Infrastructure.Repositories
{
    public interface IForexRepository
    {
        Task<Result<List<ForexRate>>> GetAll();
        Task<Result<ForexRate>?> GetByDate(string baseCurrency, DateTime dateTime);
        Task<Result<ForexRate>> Create(ForexRate forexRate);
    }
    
    public class ForexRepository : IForexRepository
    {
        private readonly IAppDbContext _dbContext;
        public ForexRepository(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<ForexRate>> Create(ForexRate forexRate)
        {
            try
            {
                _dbContext.ForexRates.Add(forexRate);
                await _dbContext.SaveChangesAsync();
                return new Result<ForexRate>
                {
                  Successful = true,
                  Data = forexRate  
                };
            } catch (Exception ex)
            {
                return new Result<ForexRate>
                {
                  Successful = false,
                  Error = Error.Exception(ex)  
                };
            }
        }

        public async Task<Result<List<ForexRate>>> GetAll()
        {
            try
            {
                var forexRates = await _dbContext.ForexRates.ToListAsync();
                return new Result<List<ForexRate>>
                {
                  Successful = true,
                  Data = forexRates  
                };
            } catch (Exception ex)
            {
                return new Result<List<ForexRate>>
                {
                  Successful = false,
                  Error = Error.Exception(ex)  
                };
            }
        }

        public async Task<Result<ForexRate>?> GetByDate(string baseCurrency, DateTime dateTime)
        {
            try
            {
                var forexRate = await _dbContext.ForexRates
                                        .Where(f => f.BaseCurrency == baseCurrency && f.RateDate == dateTime)
                                        .FirstOrDefaultAsync();
                return new Result<ForexRate>
                {
                  Successful = true,
                  Data = forexRate 
                };
            } catch(Exception ex)
            {
                return new Result<ForexRate>
                {
                  Successful = false,
                  Error = Error.Exception(ex)
                };
            }
        }
    }
}