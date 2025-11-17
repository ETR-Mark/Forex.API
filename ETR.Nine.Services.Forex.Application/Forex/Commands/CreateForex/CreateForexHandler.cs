using System;
using ETR.Nine.Services.Forex.Domain;
using ETR.Nine.Services.Forex.Infrastructure.Persistence.Database;
using ETR.Nine.Services.Forex.Infrastructure.Repositories;
using MediatR;

namespace ETR.Nine.Services.Forex.Application.Forex.Commands.CreateForex;

public class CreateForexHandler : IRequestHandler<CreateForexCommand, Result<ForexRate>>
{
    private readonly IForexRepository _forexRepository;
    public CreateForexHandler(IForexRepository forexRepository)
    {
        _forexRepository = forexRepository;
    }

    public async Task<Result<ForexRate>> Handle(CreateForexCommand request, CancellationToken cancellationToken)
    {
        var newForexRate = new ForexRate
        {
            BaseCurrency = request.BaseCurrency,
            Rate = request.Rate,
            RateDate = request.RateDate
        };

        var createdForex = await _forexRepository.Create(newForexRate);
        return Result<ForexRate>.Ok(createdForex);
    }
}