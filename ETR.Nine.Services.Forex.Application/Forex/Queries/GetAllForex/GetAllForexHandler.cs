using System;
using ETR.Nine.Mediator;
using ETR.Nine.Services.Forex.Infrastructure.Exceptions;
using ETR.Nine.Services.Forex.Infrastructure.Repositories;

namespace ETR.Nine.Services.Forex.Application.Forex.Queries.GetAllForex;

public class GetAllForexHandler : IRequestHandler<GetAllForexQuery, List<ForexListModel>>
{
    private readonly IForexRepository _forexRepository;

    public GetAllForexHandler(IForexRepository forexRepository)
    {
        _forexRepository = forexRepository;
    }

    public async Task<Result<List<ForexListModel>>> Handle(GetAllForexQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var forexRates = await _forexRepository.GetAll();
            var forexList = forexRates.Select(f => new ForexListModel
            {
                BaseCurrency = f.BaseCurrency,
                Rate = f.Rate,
                RateDate = f.RateDate,
                DateCreated = f.DateCreated
            }).ToList();

            return new Result<List<ForexListModel>>
            {
                Successful = true,
                Data = forexList
            };
        } catch (ForexApiException fe)
        {
            throw new ForexApiException("FOREX-455", fe.Message ?? "API ERROR");
        }
    }
}
