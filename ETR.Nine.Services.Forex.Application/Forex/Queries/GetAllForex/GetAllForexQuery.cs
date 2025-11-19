using System;
using ETR.Nine.Mediator;

namespace ETR.Nine.Services.Forex.Application.Forex.Queries.GetAllForex;

public class GetAllForexQuery : IRequest<List<ForexListModel>>
{
    public int PageIndex {get; set;} = 0;
    public int ItemsPerPage {get;set;} = 10;
}