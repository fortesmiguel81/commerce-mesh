using OrderService.SharedKernel;

namespace OrderService.Domain.Orders.Events;

public record OrderReturnedDomainEvent(
    Guid OrderId,
    Guid UserId,
    DateTime ReturnedOnUtc,
    string? Reason) : IDomainEvent;
