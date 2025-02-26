namespace LondonStockExchangeTrades.Infrastructure.Services;

public class InMemoryEventBus : IEventBus
{
    public async Task PublishAsync<T>(T @event)
    {
        // For MVP, log or handle synchronously. In production, we will use a message broker (e.g., RabbitMQ).
        await Task.CompletedTask;
    }
}
