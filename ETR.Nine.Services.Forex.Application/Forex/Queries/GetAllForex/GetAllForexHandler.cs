using System;
using ETR.Nine.Services.Forex.Domain;
using ETR.Nine.Services.Forex.Infrastructure.Repositories;
using MediatR;

namespace ETR.Nine.Services.Forex.Application.Forex.Queries.GetAllForex;

public class GetAllForexHandler : IRequestHandler<GetAllForexQuery, Result<List<ForexListModel>>>
{
    private readonly IForexRepository _forexRepository;
    public GetAllForexHandler(IForexRepository forexRepository)
    {
        _forexRepository = forexRepository;
    }
    public async Task<Result<List<ForexListModel>>> Handle(GetAllForexQuery request, CancellationToken cancellationToken)
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
            return Result<List<ForexListModel>>.Ok(forexList);
        } catch(Exception ex)
        {
            return Result<List<ForexListModel>>.Fail(ex.Message);
        }
    }
}
