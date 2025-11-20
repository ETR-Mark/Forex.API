using ETR.Nine.Services.Forex.Domain;
using ETR.Nine.Services.Forex.Infrastructure.Persistence.Database;
using ETR.Nine.Services.Forex.Infrastructure.Repositories;
using ETR.Nine.Services.Forex.Infrastructure.Services.Forex;

namespace ETR.Nine.Services.Forex.Infrastructure.Services
{
    public interface IForexService
    {
        Task<Result<ForexRate>> GetForexByDate(DateTime date, string baseCurrency);
    }
    
    public class ForexService : IForexService
    {
        private readonly IForexRepository _forexRepository;
        private readonly IExternalForexService _externalForexService;
        public ForexService(IForexRepository forexRepository, IExternalForexService externalForexService)
        {
            _forexRepository = forexRepository;
            _externalForexService = externalForexService;
        }

        public async Task<Result<ForexRate>> GetForexByDate(DateTime date, string baseCurrency)
        {
            try
            {
                DateTime utcNow = DateTime.UtcNow;
                var internalForexRate = await _forexRepository.GetByDate(baseCurrency, date);
                if(internalForexRate?.Data != null)
                {
                    return new Result<ForexRate>
                    {
                        Successful = true,
                        Data = internalForexRate.Data
                    };
                }

                if(date.Date > utcNow.Date){ 
                    return new Result<ForexRate?>{ Successful = false, Error = Error.Validation("FOREX-455", "Invalid Date") };
                }

                if(date.Date == utcNow.Date)
                {
                    var currencyToday = await _externalForexService.GetCurrencyRateTodayAsync(baseCurrency);
                    var errorMsg = currencyToday.Error?.Message ?? "Unknown external API error";
                    if (!currencyToday.Successful || currencyToday.Data == null)
                        return new Result<ForexRate>
                        {
                            Successful = false,
                            Error = Error.Problem("FOREX-455", errorMsg)
                        };


                    if (currencyToday.Data.Rates.ContainsKey("PHP"))
                    {
                        var createdForexToday = await _forexRepository.Create(new ForexRate
                        {
                            BaseCurrency = baseCurrency,
                            Rate = currencyToday.Data.Rates["PHP"], 
                            RateDate = utcNow.Date
                        });

                        return new Result<ForexRate>
                        {
                            Successful = true,
                            Data = createdForexToday.Data
                        };
                    }
                    else
                    {
                        return new Result<ForexRate>
                        {
                            Successful = false,
                            Error = Error.NotFound("FOREX-455", "External API Error")
                        };
                    }
                }
                var currency = await _externalForexService.GetCurrencyRateAsync(date, baseCurrency);
                if (!currency.Successful || currency.Data == null) return new Result<ForexRate>{ Successful = false, Data = null, Error = Error.Problem("FOREX-455", currency.Error.Message) };

                if (currency.Data.Rates.ContainsKey("PHP"))
                {
                    var createdForex = await _forexRepository.Create(new ForexRate
                    {
                        BaseCurrency = baseCurrency,
                        Rate = currency.Data.Rates["PHP"],
                        RateDate = DateTimeOffset.FromUnixTimeSeconds(currency.Data.Timestamp).UtcDateTime.Date
                    });

                    return new Result<ForexRate>
                    {
                        Successful = true,
                        Data = createdForex.Data
                    };
                }
                else
                {
                    return new Result<ForexRate>
                    {
                        Successful = false,
                        Error = Error.NotFound("FOREX-455", "External API Error")
                    };
                }
                
            } 
            catch(Exception ex)
            {
                return new Result<ForexRate>
                {
                    Successful = false,
                    Error = Error.Exception(ex)
                };
            }
        }
    }
}

