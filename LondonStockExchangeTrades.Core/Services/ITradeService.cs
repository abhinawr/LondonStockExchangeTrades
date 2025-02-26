using LondonStockExchangeTrades.Domain.DTOs;
using LondonStockExchangeTrades.Domain.Models;

namespace LondonStockExchangeTrades.Core.Services;

public interface ITradeService
{
    Task ProcessTradeAsync(Trade trade);
    Task<decimal> GetStockValueAsync(string tickerSymbol);
    Task<List<StockValueDto>> GetAllStockValuesAsync();
    Task<List<StockValueDto>> GetStockValuesRangeAsync(List<string> tickerSymbols);
}
