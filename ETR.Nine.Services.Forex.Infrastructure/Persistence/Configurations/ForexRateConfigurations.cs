using ETR.Nine.Services.Forex.Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETR.Nine.Services.Forex.Infrastructure.Persistence.Configurations
{
    public class ForexRateConfigurations : IEntityTypeConfiguration<ForexRate>
    {
        public void Configure(EntityTypeBuilder<ForexRate> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("ForexRates");

            entity.Property(e => e.BaseCurrency)
                .HasColumnName("BaseCurrency")
                .IsRequired();

            entity.Property(e => e.Rate)
                .HasColumnName("Rates")
                .IsRequired();

            entity.Property(e => e.DateCreated)
                .HasColumnName("DateCreated")
                .IsRequired();
        }
    }
}