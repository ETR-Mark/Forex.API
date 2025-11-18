using System;
using ETR.Nine.Mediator;
using ETR.Nine.Services.Forex.Infrastructure.Persistence.Database;

namespace ETR.Nine.Services.Forex.Application.Forex.Commands.CreateForex;

public class CreateForexCommand : IRequest<ForexRate>
{
    public string BaseCurrency { get; set; } = string.Empty;
    public decimal Rate { get; set; }
    public DateTime RateDate { get; set; }
}
