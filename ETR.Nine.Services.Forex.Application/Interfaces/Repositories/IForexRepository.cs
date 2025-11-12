using ETR.Nine.Services.Forex.Domain.Entity;

namespace ETR.Nine.Services.Forex.Application.Interfaces.Repositories
{
    public interface IForexRepository
    {
        Task<ForexRate?> GetByDate(DateTime dateTime);
        Task<ForexRate> Create(ForexRate forexRate);
    }
}