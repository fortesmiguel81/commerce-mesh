using OrderService.SharedKernel;

namespace OrderService.Domain.Orders.Events;

public record OrderProcessingStartedDomainEvent(
    Guid OrderId,
    Guid UserId,
    DateTime ProcessingOnUtc) : IDomainEvent;
