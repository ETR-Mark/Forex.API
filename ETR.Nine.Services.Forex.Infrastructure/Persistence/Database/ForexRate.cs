namespace ETR.Nine.Services.Forex.Infrastructure.Persistence.Database
{
    public class ForexRate
    {
        public int Id { get; set; }
        public string BaseCurrency { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public DateTime RateDate { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow.Date;
    }
}