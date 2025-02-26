namespace LondonStockExchangeTrades.Domain.DTOs;

public class TradeDTO
{
    public string TickerSymbol { get; set; } = default!;
    public decimal Price { get; set; }
    public decimal ShareVolume { get; set; }
    public string BrokerId { get; set; } = default!;
}
