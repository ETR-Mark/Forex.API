using System;

namespace ETR.Nine.Services.Forex.API.Models;

public class ForexRateModel
{
    public int Id { get; set; }
    public string BaseCurrency { get; set; } = null!;
    public decimal Rate { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}
