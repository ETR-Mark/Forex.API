using ETR.Nine.Services.Forex.Domain;
using ETR.Nine.Services.Forex.Infrastructure.Persistence.Database;
using ETR.Nine.Services.Forex.Infrastructure.Repositories;
using ETR.Nine.Services.Forex.Infrastructure.Services.Forex;

namespace ETR.Nine.Services.Forex.Infrastructure.Services
{
    public interface IForexService
    {
        Task<Result<List<ForexRate>>> GetAllForex();
        Task<Result<ForexRate?>> GetForexByDate(DateTime date, string baseCurrency);
        Task<Result<ForexRate>> CreateForexRate(ForexRate forexRate);
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

        public async Task<Result<ForexRate>> CreateForexRate(ForexRate forexRate)
        {
            try
            {
                var newForexRate = await _forexRepository.Create(forexRate);
                return Result<ForexRate>.Ok(newForexRate);
            } catch (Exception ex)
            {
                return Result<ForexRate>.Fail(ex.Message);
            }
        }

        public async Task<Result<List<ForexRate>>> GetAllForex()
        {
            try
            {
                var forexRates = await _forexRepository.GetAll();
                return Result<List<ForexRate>>.Ok(forexRates);
            }
            catch (Exception ex)
            {
                return Result<List<ForexRate>>.Fail(ex.Message);
            }
        }

        public async Task<Result<ForexRate?>> GetForexByDate(DateTime date, string baseCurrency)
        {
            try
            {
                var internalForexRate = await _forexRepository.GetByDate(baseCurrency, date);
                if(internalForexRate == null)
                {
                    DateTime utcNow = DateTime.UtcNow;
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

                            return Result<ForexRate>.Ok(createdForexToday);
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

                        return Result<ForexRate>.Ok(createdForex);
                    }
                }

                return Result<ForexRate>.Ok(internalForexRate);
            } catch(Exception ex)
            {
                return Result<ForexRate>.Fail(ex.Message);
            }
        }
    }
}

