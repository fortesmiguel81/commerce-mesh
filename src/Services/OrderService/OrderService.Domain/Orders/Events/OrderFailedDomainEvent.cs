using OrderService.SharedKernel;

namespace OrderService.Domain.Orders.Events;

public record OrderFailedDomainEvent(
    Guid OrderId,
    Guid UserId,
    DateTime FailedOnUtc,
    string Reason) : IDomainEvent;
