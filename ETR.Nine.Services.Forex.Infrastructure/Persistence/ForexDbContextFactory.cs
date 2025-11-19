using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace ETR.Nine.Services.Forex.Infrastructure.Persistence
{
    public class ForexDbContextFactory : IDesignTimeDbContextFactory<ForexDbContext>
    {
        public ForexDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ForexDbContext>();
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__SQLiteConnection")
                       ?? "Data Source=ETRForex.db";
            builder.UseSqlite(connectionString);

            return new ForexDbContext(builder.Options);
        }
    }
}