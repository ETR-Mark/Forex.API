using System;
using ETR.Nine.Services.Forex.Domain;
using ETR.Nine.Services.Forex.Infrastructure.Persistence.Database;
using MediatR;

namespace ETR.Nine.Services.Forex.Application.Forex.Commands.CreateForex;

public class CreateForexCommand : IRequest<Result<ForexRate>>
{
    public string BaseCurrency { get; set; } = string.Empty;
    public decimal Rate { get; set; }
    public DateTime RateDate { get; set; }
}
