namespace ETR.Nine.Services.Forex.Domain.Entity
{
    public class ForexRate
    {
        public int Id { get; set; }
        public string BaseCurrency { get; set; } = null!;
        public decimal Rate { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}