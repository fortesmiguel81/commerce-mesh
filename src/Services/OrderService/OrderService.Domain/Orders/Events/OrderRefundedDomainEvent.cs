using OrderService.SharedKernel;

namespace OrderService.Domain.Orders.Events;

public record OrderRefundedDomainEvent(
    Guid OrderId,
    Guid UserId,
    DateTime RefundedOnUtc,
    string? Reason) : IDomainEvent;
