using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Orders;

namespace OrderService.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Order> Orders { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
