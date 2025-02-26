using LondonStockExchangeTrades.Domain.DTOs;
using LondonStockExchangeTrades.Domain.Events;
using LondonStockExchangeTrades.Domain.Models;
using LondonStockExchangeTrades.Infrastructure.Caching;
using LondonStockExchangeTrades.Infrastructure.Repositories;
using LondonStockExchangeTrades.Infrastructure.Services;

namespace LondonStockExchangeTrades.Core.Services;

public class TradeService : ITradeService
{
    private readonly ITradeRepository _tradeRepository;
    private readonly IEventBus _eventBus;
    private readonly ICacheService _cacheService;

    public TradeService(ITradeRepository tradeRepository, IEventBus eventBus, ICacheService cacheService)
    {
        _tradeRepository = tradeRepository;
        _eventBus = eventBus;
        _cacheService = cacheService;
    }

    public async Task ProcessTradeAsync(Trade trade)
    {
        await _tradeRepository.AddTradeAsync(trade);
        await _eventBus.PublishAsync(new TradeProcessedEvent { Trade = trade });
        await UpdateStockCacheAsync(trade.TickerSymbol);
    }

    public async Task<decimal> GetStockValueAsync(string tickerSymbol)
    {
        var cachedValue = await _cacheService.GetAsync<decimal?>($"stock:{tickerSymbol}");
        if (cachedValue != null) return cachedValue.Value;

        var avgPrice = await _tradeRepository.GetAveragePriceAsync(tickerSymbol);
        await _cacheService.SetAsync($"stock:{tickerSymbol}", avgPrice, TimeSpan.FromMinutes(5));
        return avgPrice;
    }

    public async Task<List<StockValueDto>> GetAllStockValuesAsync()
    {
        var stocks = await _tradeRepository.GetAllStockSummariesAsync();
        return stocks.Select(s => new StockValueDto { TickerSymbol = s.Key, AveragePrice = s.Value }).ToList();
    }

    public async Task<List<StockValueDto>> GetStockValuesRangeAsync(List<string> tickerSymbols)
    {
        var results = new List<StockValueDto>();
        foreach (var ticker in tickerSymbols)
        {
            var avgPrice = await GetStockValueAsync(ticker);
            results.Add(new StockValueDto { TickerSymbol = ticker, AveragePrice = avgPrice });
        }
        return results;
    }

    private async Task UpdateStockCacheAsync(string tickerSymbol)
    {
        var avgPrice = await _tradeRepository.GetAveragePriceAsync(tickerSymbol);
        await _cacheService.SetAsync($"stock:{tickerSymbol}", avgPrice, TimeSpan.FromMinutes(5));
    }
}
