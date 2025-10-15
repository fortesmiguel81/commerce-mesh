namespace OrderService.Application.Orders;

public class CreateOrderItemCommand
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; } = string.Empty;
    public decimal UnitPrice { get; init; }
    public string Currency { get; init; } = string.Empty;
    public int Quantity { get; init; }
}
