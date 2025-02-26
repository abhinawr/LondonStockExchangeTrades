namespace LondonStockExchangeTrades.Domain.DTOs;

public class StockValueDto
{
    public string TickerSymbol { get; set; } = default!;
    public decimal AveragePrice { get; set; }
}
