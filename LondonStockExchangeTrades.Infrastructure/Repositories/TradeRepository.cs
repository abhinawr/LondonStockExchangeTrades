using LondonStockExchangeTrades.Domain.Models;
using LondonStockExchangeTrades.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LondonStockExchangeTrades.Infrastructure.Repositories;

public class TradeRepository : ITradeRepository
{
    private readonly TradeDbContext _context;

    public TradeRepository(TradeDbContext context)
    {
        _context = context;
    }

    public async Task AddTradeAsync(Trade trade)
    {
        _context.Trades.Add(trade);
        await _context.SaveChangesAsync();
    }

    public async Task<decimal> GetAveragePriceAsync(string tickerSymbol)
    {
        return await _context.Trades
            .Where(t => t.TickerSymbol == tickerSymbol)
            .AverageAsync(t => t.Price);
    }

    public async Task<Dictionary<string, decimal>> GetAllStockSummariesAsync()
    {
        return await _context.Trades
            .GroupBy(t => t.TickerSymbol)
            .Select(g => new { Ticker = g.Key, AvgPrice = g.Average(t => t.Price) })
            .ToDictionaryAsync(x => x.Ticker, x => x.AvgPrice);
    }
}
