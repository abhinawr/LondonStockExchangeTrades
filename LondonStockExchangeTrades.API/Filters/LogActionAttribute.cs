using Microsoft.AspNetCore.Mvc;

namespace LondonStockExchangeTrades.API.Filters;

public class LogActionAttribute : TypeFilterAttribute
{
    public LogActionAttribute() : base(typeof(LoggingActionFilter))
    {
    }
}
