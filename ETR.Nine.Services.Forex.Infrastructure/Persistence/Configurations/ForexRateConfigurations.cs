using ETR.Nine.Services.Forex.Domain.Entity;
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
                .HasColumnType("VARCHAR(25)")
                .IsRequired();

            entity.Property(e => e.Rate)
                .HasColumnName("Rates")
                .HasColumnType("DECIMAL(18,2)")
                .IsRequired();

            entity.Property(e => e.DateCreated)
                .HasColumnName("DateCreated")
                .HasColumnType("DATE")
                .IsRequired();
        }
    }
}