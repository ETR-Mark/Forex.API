using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace ETR.Nine.Services.Forex.Infrastructure.Persistence
{
    public class ForexDbContextFactory : IDesignTimeDbContextFactory<ForexDbContext>
    {
        public ForexDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "ETR.Nine.Services.Forex.API");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(basePath))
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ForexDbContext>();
            var connectionString = configuration.GetConnectionString("SQLiteConnection");
            builder.UseSqlite(connectionString);

            return new ForexDbContext(builder.Options);
        }
    }
}