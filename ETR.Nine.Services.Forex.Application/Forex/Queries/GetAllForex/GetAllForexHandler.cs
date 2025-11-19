using System;
using ETR.Nine.Mediator;
using ETR.Nine.Services.Forex.Infrastructure.Exceptions;
using ETR.Nine.Services.Forex.Infrastructure.Repositories;

namespace ETR.Nine.Services.Forex.Application.Forex.Queries.GetAllForex;

public interface IGetAllForexHandler
{
    Task<Result<List<ForexListModel>>> Handle(GetAllForexQuery request, CancellationToken cancellationToken = default);
}


public class GetAllForexHandler : IGetAllForexHandler
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
            var pagedForexRates = forexRates
                .OrderBy(forex => forex.RateDate)
                .Skip((request.PageIndex - 1) * request.ItemsPerPage)
                .Take(request.ItemsPerPage)
                .ToList();
            
            var forexList = pagedForexRates.Select(forex => new ForexListModel
            {
                BaseCurrency = forex.BaseCurrency,
                Rate = forex.Rate,
                RateDate = forex.RateDate,
                DateCreated = forex.DateCreated
            }).ToList();

            return new Result<List<ForexListModel>>
            {
                Successful = true,
                Data = forexList
            };

        } catch (ForexApiException fe)
        {
            return new Result<List<ForexListModel>>
                {
                    Successful = false,
                    Error = new Error
                    {
                        Code = "FOREX-455",
                        Message = fe.Message
                    },
                };
        }
    }
}
