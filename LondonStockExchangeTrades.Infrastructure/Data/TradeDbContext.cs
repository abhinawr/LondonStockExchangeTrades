using LondonStockExchangeTrades.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LondonStockExchangeTrades.Infrastructure.Data;

public class TradeDbContext : DbContext
{
    public TradeDbContext(DbContextOptions<TradeDbContext> options) : base(options)
    {
    }

    public DbSet<Trade> Trades { get; set; }
}