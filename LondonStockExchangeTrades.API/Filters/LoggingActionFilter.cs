using Microsoft.AspNetCore.Mvc.Filters;

namespace LondonStockExchangeTrades.API.Filters;

public class LoggingActionFilter : IActionFilter
{
    private readonly ILogger<LoggingActionFilter> _logger;

    public LoggingActionFilter(ILogger<LoggingActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation($"Executing action: {context.ActionDescriptor.DisplayName}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation($"Executed action: {context.ActionDescriptor.DisplayName}");
    }
}
