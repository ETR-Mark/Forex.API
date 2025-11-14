using System;

namespace ETR.Nine.Services.Forex.Application.Models;

public class SuccessResponse
{
    public List<CurrencyRateItem> Data {get;set;} = new();

    public SuccessResponse(string from, string to, decimal rate)
    {
        Data.Add(new CurrencyRateItem
        {
            From = from,
            To = to,
            Rate = rate
        });
    }
}

public class CurrencyRateItem
{
    public string From {get;set;} = string.Empty;
    public string To {get;set;} = string.Empty;
    public decimal Rate {get; set;}
}
