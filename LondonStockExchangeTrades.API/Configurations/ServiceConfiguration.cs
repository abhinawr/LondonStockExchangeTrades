using LondonStockExchangeTrades.Core.Services;
using LondonStockExchangeTrades.Infrastructure.Caching;
using LondonStockExchangeTrades.Infrastructure.Repositories;
using LondonStockExchangeTrades.Infrastructure.Services;

namespace LondonStockExchangeTrades.API.Configurations;

public static class ServiceConfiguration
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITradeService, TradeService>();

        services.AddScoped<ITradeRepository, TradeRepository>();

        services.AddScoped<ICacheService, InMemoryCacheService>();

        services.AddScoped<IEventBus, InMemoryEventBus>();

        return services;
    }
}
