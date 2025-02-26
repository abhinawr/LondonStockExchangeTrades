using LondonStockExchangeTrades.API.Filters;
using LondonStockExchangeTrades.Core.Services;
using LondonStockExchangeTrades.Domain.DTOs;
using LondonStockExchangeTrades.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LondonStockExchangeTrades.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TradesController : ControllerBase
{
    private readonly ITradeService _tradeService;

    public TradesController(ITradeService tradeService)
    {
        _tradeService = tradeService;
    }

    [HttpPost]
    [LogAction]
    public async Task<IActionResult> SubmitTrade([FromBody] TradeDTO request)
    {
        var trade = new Trade
        {
            TickerSymbol = request.TickerSymbol,
            Price = request.Price,
            ShareVolume = request.ShareVolume,
            BrokerId = request.BrokerId,
            Timestamp = DateTime.UtcNow
        };
        await _tradeService.ProcessTradeAsync(trade);
        return Ok();
    }

    [HttpGet("stocks/{tickerSymbol}")]
    [LogAction]
    public async Task<IActionResult> GetStockValue(string tickerSymbol)
    {
        var value = await _tradeService.GetStockValueAsync(tickerSymbol);
        return Ok(new StockValueDto { TickerSymbol = tickerSymbol, AveragePrice = value });
    }

    [HttpGet("stocks")]
    [LogAction]
    public async Task<IActionResult> GetAllStockValues()
    {
        var values = await _tradeService.GetAllStockValuesAsync();
        return Ok(values);
    }

    [HttpPost("stocks/range")]
    [LogAction]
    public async Task<IActionResult> GetStockValuesRange([FromBody] List<string> tickerSymbols)
    {
        var values = await _tradeService.GetStockValuesRangeAsync(tickerSymbols);
        return Ok(values);
    }
}