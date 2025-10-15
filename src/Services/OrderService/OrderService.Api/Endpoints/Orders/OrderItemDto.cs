namespace OrderService.Api.Endpoints.Orders;

public sealed class OrderItemDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public string Currency { get; set; }
    public int Quantity { get; set; }
}
