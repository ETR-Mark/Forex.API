using System;
using ETR.Nine.Mediator;
using ETR.Nine.Services.Forex.Domain;
using ETR.Nine.Services.Forex.Infrastructure.Exceptions;
using ETR.Nine.Services.Forex.Infrastructure.Persistence.Database;
using ETR.Nine.Services.Forex.Infrastructure.Repositories;

namespace ETR.Nine.Services.Forex.Application.Forex.Commands.CreateForex;

public class CreateForexHandler : IRequestHandler<CreateForexCommand, ForexRate>
{
    private readonly IForexRepository _forexRepository;
    public CreateForexHandler(IForexRepository forexRepository)
    {
        _forexRepository = forexRepository;
    }

    public async Task<Result<ForexRate>> Handle(CreateForexCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var newForexRate = new ForexRate
            {
                BaseCurrency = request.BaseCurrency,
                Rate = request.Rate,
                RateDate = request.RateDate 
            };

            var createdForex = await _forexRepository.Create(newForexRate);
            return new Result<ForexRate>
            {
                Successful = true,
                Data = createdForex
            };
        } catch(ForexApiException fe)
        {
            throw new ForexApiException("FOREX-455", fe.Message ?? "CREATE FOREX COMMAND ERROR");
        }
    }
}