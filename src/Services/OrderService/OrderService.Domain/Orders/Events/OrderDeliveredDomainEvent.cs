using OrderService.SharedKernel;

namespace OrderService.Domain.Orders.Events;

public record OrderDeliveredDomainEvent(
    Guid OrderId,
    Guid UserId,
    DateTime DeliveredOnUtc) : IDomainEvent;
