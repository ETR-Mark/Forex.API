using ETR.Nine.Services.Forex.Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace ETR.Nine.Services.Forex.Infrastructure.Persistence
{
    public interface IAppDbContext
    {
        DbSet<ForexRate> ForexRates { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}