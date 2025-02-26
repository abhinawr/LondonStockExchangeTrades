using LondonStockExchangeTrades.Domain.Models;

namespace LondonStockExchangeTrades.Domain.Events;

public class TradeProcessedEvent
{
    public Trade Trade { get; set; } = new();
}
