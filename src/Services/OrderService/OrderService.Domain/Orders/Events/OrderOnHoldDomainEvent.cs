using OrderService.SharedKernel;

namespace OrderService.Domain.Orders.Events;

public record OrderOnHoldDomainEvent(
    Guid OrderId,
    Guid UserId,
    DateTime OnHoldOnUtc,
    string Reason) : IDomainEvent;
