using ETR.Nine.Services.Forex.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace ETR.Nine.Services.Forex.Infrastructure.Persistence
{
    public class ForexDbContext : DbContext, IAppDbContext
    {
        public ForexDbContext(DbContextOptions options) : base(options) { }
        public DbSet<ForexRate> ForexRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ForexDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}