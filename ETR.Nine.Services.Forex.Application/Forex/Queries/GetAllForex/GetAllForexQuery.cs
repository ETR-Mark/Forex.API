using System;
using ETR.Nine.Services.Forex.Domain;
using MediatR;

namespace ETR.Nine.Services.Forex.Application.Forex.Queries.GetAllForex;

public class GetAllForexQuery : IRequest<Result<List<ForexListModel>>>{}
