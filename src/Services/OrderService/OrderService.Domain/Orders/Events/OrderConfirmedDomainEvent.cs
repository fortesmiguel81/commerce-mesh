using OrderService.SharedKernel;

namespace OrderService.Domain.Orders.Events;

public record OrderConfirmedDomainEvent(
    Guid OrderId,
    Guid UserId,
    DateTime ConfirmedOnUtc) : IDomainEvent;
