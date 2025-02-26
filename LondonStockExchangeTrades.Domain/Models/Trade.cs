namespace LondonStockExchangeTrades.Domain.Models;

public class Trade
{
    public int Id { get; set; }
    public string TickerSymbol { get; set; } = default!;
    public decimal Price { get; set; }
    public decimal ShareVolume { get; set; }
    public string BrokerId { get; set; } = default!;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
