using OrderService.Application.Abstractions.Messaging;

namespace OrderService.Application.Orders;

public class CreateOrderCommand : ICommand<Guid>
{
    public Guid UserId { get; init; }
    public List<CreateOrderItemCommand> Items { get; init; } = [];
    public string Currency { get; init; } = string.Empty;
    public int Priority { get; init; }
}
