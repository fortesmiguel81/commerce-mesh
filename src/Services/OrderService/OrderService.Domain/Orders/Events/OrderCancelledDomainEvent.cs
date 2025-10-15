using OrderService.SharedKernel;

namespace OrderService.Domain.Orders.Events;

public record OrderCancelledDomainEvent(
    Guid OrderId,
    Guid UserId,
    DateTime CancelledOnUtc,
    string? Reason) : IDomainEvent;
