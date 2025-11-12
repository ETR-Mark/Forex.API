using ETR.Nine.Services.Forex.Domain.Entity;

namespace ETR.Nine.Services.Forex.Application.Interfaces.IServices
{
    public interface IForexService
    {
        Task<ForexRate> GetForexByDate(DateTime dateTime);
        Task<ForexRate> CreateForexRate(ForexRate forexRate);
    }
}