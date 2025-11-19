using ETR.Nine.Services.Forex.Domain;
using ETR.Nine.Services.Forex.Infrastructure.Exceptions;
using ETR.Nine.Services.Forex.Infrastructure.Persistence.Database;
using ETR.Nine.Services.Forex.Infrastructure.Repositories;
using ETR.Nine.Services.Forex.Infrastructure.Services.Forex;

namespace ETR.Nine.Services.Forex.Infrastructure.Services
{
    public interface IForexService
    {
        Task<Result<ForexRate?>> GetForexByDate(DateTime date, string baseCurrency);
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

        public async Task<Result<ForexRate?>> GetForexByDate(DateTime date, string baseCurrency)
        {
            try
            {
                var internalForexRate = await _forexRepository.GetByDate(baseCurrency, date);
                if(internalForexRate == null)
                {
                    DateTime utcNow = DateTime.UtcNow;
                    if(date.Date > utcNow.Date) throw new ForexApiException("FOREX-455", "Invalid Date!");
                    if(date.Date == utcNow.Date)
                    {
                        var currencyToday = await _externalForexService.GetCurrencyRateTodayAsync(baseCurrency);

                        if (currencyToday.Rates.ContainsKey("PHP"))
                        {
                            var createdForexToday = await _forexRepository.Create(new ForexRate
                            {
                                BaseCurrency = baseCurrency,
                                Rate = currencyToday.Rates["PHP"], 
                                RateDate = utcNow.Date
                            });

                            return new Result<ForexRate?>
                            {
                              Successful = true,
                              Data = createdForexToday
                            };
                        }
                        else
                        {
                            throw new ForexApiException("FOREX-455", $"Rate for PHP was not found for the specified date.");
                        }
                    }
                    var currency = await _externalForexService.GetCurrencyRateAsync(date, baseCurrency);

                    if (currency.Rates.ContainsKey("PHP"))
                    {
                        var createdForex = await _forexRepository.Create(new ForexRate
                        {
                            BaseCurrency = baseCurrency,
                            Rate = currency.Rates["PHP"],
                            RateDate = DateTimeOffset.FromUnixTimeSeconds(currency.Timestamp).UtcDateTime.Date
                        });

                        return new Result<ForexRate?>
                        {
                            Successful = true,
                            Data = createdForex
                        };
                    }
                    else
                    {
                        throw new ForexApiException("FOREX-455", $"Rate for PHP was not found for the specified date.");
                    }
                }

                return new Result<ForexRate?>
                {
                    Successful = true,
                    Data = internalForexRate
                };
            } catch(ForexApiException fe)
            {
                throw;
            }
            catch(Exception ex)
            {
                return new Result<ForexRate?>
                {
                    Successful = false,
                    Error = new Error
                    {
                        Code = "FOREX-455",
                        Message = ex.Message
                    }
                };
            }
        }
    }
}

