using ETR.Nine.Services.Forex.Infrastructure.Persistence.Database;

namespace ETR.Nine.Services.Forex.Infrastructure.Repositories
{
    public interface IForexRepository
    {
        Task<List<ForexRate>> GetAll();
        Task<ForexRate?> GetByDate(DateTime dateTime);
        Task<ForexRate> Create(ForexRate forexRate);
    }
}