using LondonStockExchangeTrades.API.Middleware;

namespace LondonStockExchangeTrades.API.Configurations;

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
