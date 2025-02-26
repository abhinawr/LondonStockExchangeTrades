namespace LondonStockExchangeTrades.Infrastructure.Services;

public interface IEventBus
{
    Task PublishAsync<T>(T @event);
}
