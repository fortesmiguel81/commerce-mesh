using OrderService.Domain.Shared;
using OrderService.SharedKernel;

namespace OrderService.Domain.Orders.Events;

public record OrderCreatedDomainEvent(
    Guid OrderId,
    Guid UserId,
    DateTime OrderDate,
    Money TotalAmount,
    Priority Priority,
    IReadOnlyCollection<OrderItem> Items) : IDomainEvent;
