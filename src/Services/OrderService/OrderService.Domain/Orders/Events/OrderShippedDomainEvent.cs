using OrderService.SharedKernel;

namespace OrderService.Domain.Orders.Events;

public record OrderShippedDomainEvent(
    Guid OrderId,
    Guid UserId,
    DateTime ShippedOnUtc) : IDomainEvent;
