using OrderService.SharedKernel;
using OrderService.Domain.Shared;

namespace OrderService.Domain.Orders;

public class OrderItem : Entity
{
    public Guid ProductId { get; private set; }
    public string ProductReference { get; private set; }
    public Money UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public Money Subtotal => UnitPrice with { Amount = UnitPrice.Amount * Quantity };

    private OrderItem() { }

    public OrderItem(Guid productId, string productReference, Money unitPrice, int quantity)
    {
        ProductId = productId;
        ProductReference = productReference;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }
}
