using System;

namespace ETR.Nine.Services.Forex.Application.Forex;

public class ForexListModel
{
    public string BaseCurrency { get; set; } = string.Empty;
    public decimal Rate { get; set; }
    public DateTime RateDate { get; set; }
    public DateTime? DateCreated {get; set;}
}