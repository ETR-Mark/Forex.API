using System;

namespace ETR.Nine.Services.Forex.Domain.Models;

public class ForexRateModel
{
    public int Id { get; set; }
    public string BaseCurrency { get; private set; } = string.Empty;
    public decimal Rate { get; private set; }
    public DateTime RateDate { get; private set; }
}
