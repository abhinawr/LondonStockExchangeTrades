using LondonStockExchangeTrades.Domain.Models;

namespace LondonStockExchangeTrades.Infrastructure.Repositories;

public interface ITradeRepository
{
    Task AddTradeAsync(Trade trade);
    Task<decimal> GetAveragePriceAsync(string tickerSymbol);
    Task<Dictionary<string, decimal>> GetAllStockSummariesAsync();
}
