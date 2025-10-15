using OrderService.Domain.Shared;
using OrderService.SharedKernel;
using OrderService.Domain.Orders.Events;

namespace OrderService.Domain.Orders;

public class Order : Entity
{
    public Order(
        Guid id,
        Guid userId,
        DateTime orderDate,
        Money totalAmount,
        OrderStatus status,
        Priority priority,
        DateTime createdOnUtc)
        : base(id)
    {
        UserId = userId;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
        Status = status;
        Priority = priority;
        CreatedOnUtc = createdOnUtc;
    }

    private Order() { }

    public Guid UserId { get; private set; }

    public DateTime OrderDate { get; private set; }

    public Money TotalAmount { get; private set; }

    public OrderStatus Status { get; private set; }

    public Priority Priority { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime? ConfirmedOnUtc { get; private set; }

    public DateTime? ProcessingOnUtc { get; private set; }

    public DateTime? ShippedOnUtc { get; private set; }

    public DateTime? DeliveredOnUtc { get; private set; }

    public DateTime? CancelledOnUtc { get; private set; }

    public DateTime? ReturnedOnUtc { get; private set; }

    public DateTime? RefundedOnUtc { get; private set; }

    public DateTime? FailedOnUtc { get; private set; }

    public DateTime? OnHoldOnUtc { get; private set; }

    private readonly List<OrderItem> _items = new();

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    private void RecalculateTotalAmount()
    {
        TotalAmount = _items.Aggregate(Money.Zero(), (sum, item) => sum + item.Subtotal);
    }

    public static Order Create(
        Guid userId,
        DateTime orderDate,
        Priority priority,
        List<OrderItem> items)
    {
        Money totalAmount = items
            .Select(i => i.Subtotal)
            .Aggregate(Money.Zero(), (sum, next) => sum + next);

        var order = new Order(
            Guid.NewGuid(),
            userId,
            orderDate,
            totalAmount,
            OrderStatus.Pending,
            priority,
            DateTime.UtcNow);

        foreach (OrderItem item in items)
        {
            order._items.Add(item);
        }

        order.RaiseDomainEvent(
            new OrderCreatedDomainEvent(
                order.Id,
                userId,
                orderDate,
                totalAmount,
                priority,
                items.AsReadOnly()));

        return order;
    }

    public Result Confirm(DateTime utcNow)
    {
        if (Status != OrderStatus.Pending)
        {
            return Result.Failure(OrderErrors.NotPending);
        }

        Status = OrderStatus.Confirmed;
        ConfirmedOnUtc = utcNow;

        RaiseDomainEvent(new OrderConfirmedDomainEvent(Id, UserId, ConfirmedOnUtc.Value));

        return Result.Success();
    }

    public void StartProcessing()
    {
        if (Status != OrderStatus.Confirmed)
        {
            throw new InvalidOperationException("Only confirmed orders can start processing.");
        }

        Status = OrderStatus.Processing;
        ProcessingOnUtc = DateTime.UtcNow;

        RaiseDomainEvent(new OrderProcessingStartedDomainEvent(Id, UserId, ProcessingOnUtc.Value));
    }

    public void Ship()
    {
        if (Status != OrderStatus.Processing)
        {
            throw new InvalidOperationException("Only processing orders can be shipped.");
        }

        Status = OrderStatus.Shipped;
        ShippedOnUtc = DateTime.UtcNow;

        RaiseDomainEvent(new OrderShippedDomainEvent(Id, UserId, ShippedOnUtc.Value));
    }

    public void Deliver()
    {
        if (Status != OrderStatus.Shipped)
        {
            throw new InvalidOperationException("Only shipped orders can be delivered.");
        }

        Status = OrderStatus.Delivered;
        DeliveredOnUtc = DateTime.UtcNow;

        RaiseDomainEvent(new OrderDeliveredDomainEvent(Id, UserId, DeliveredOnUtc.Value));
    }

    public void Cancel(string? reason = null)
    {
        if (Status == OrderStatus.Delivered || Status == OrderStatus.Cancelled)
        {
            throw new InvalidOperationException("Cannot cancel delivered or already cancelled orders.");
        }

        Status = OrderStatus.Cancelled;
        CancelledOnUtc = DateTime.UtcNow;

        RaiseDomainEvent(new OrderCancelledDomainEvent(Id, UserId, CancelledOnUtc.Value, reason));
    }

    public void MarkAsFailed(string reason)
    {
        if (Status == OrderStatus.Delivered || Status == OrderStatus.Cancelled)
        {
            throw new InvalidOperationException("Cannot mark delivered or cancelled orders as failed.");
        }

        Status = OrderStatus.Failed;
        FailedOnUtc = DateTime.UtcNow;

        RaiseDomainEvent(new OrderFailedDomainEvent(Id, UserId, FailedOnUtc.Value, reason));
    }

    public void Return(string? reason = null)
    {
        if (Status != OrderStatus.Delivered)
        {
            throw new InvalidOperationException("Only delivered orders can be returned.");
        }

        Status = OrderStatus.Returned;
        ReturnedOnUtc = DateTime.UtcNow;

        RaiseDomainEvent(new OrderReturnedDomainEvent(Id, UserId, ReturnedOnUtc.Value, reason));
    }

    public void Refund(string? reason = null)
    {
        if (Status != OrderStatus.Returned && Status != OrderStatus.Cancelled)
        {
            throw new InvalidOperationException("Only returned or cancelled orders can be refunded.");
        }

        Status = OrderStatus.Refunded;
        RefundedOnUtc = DateTime.UtcNow;

        RaiseDomainEvent(new OrderRefundedDomainEvent(Id, UserId, RefundedOnUtc.Value, reason));
    }

    public void PutOnHold(string reason)
    {
        if (Status == OrderStatus.Delivered || Status == OrderStatus.Cancelled || Status == OrderStatus.Failed)
        {
            throw new InvalidOperationException("Cannot put delivered, cancelled, or failed orders on hold.");
        }

        Status = OrderStatus.OnHold;
        OnHoldOnUtc = DateTime.UtcNow;

        RaiseDomainEvent(new OrderOnHoldDomainEvent(Id, UserId, OnHoldOnUtc.Value, reason));
    }
}
