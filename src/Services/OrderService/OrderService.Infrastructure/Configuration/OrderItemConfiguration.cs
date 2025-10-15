using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Orders;
using OrderService.Domain.Shared;

namespace OrderService.Infrastructure.Configuration;

internal sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {

        // Configure table name
        builder.ToTable("orderItems");

        builder.HasKey(orderItem => orderItem.Id);

        builder.Property(orderItem => orderItem.ProductId)
            .IsRequired();

        builder.Property(orderItem => orderItem.ProductReference)
            .HasMaxLength(255)
            .IsRequired();

        // Configure Money value object for UnitPrice
        builder.OwnsOne(orderItem => orderItem.UnitPrice, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code,
                    code => Currency.FromCode(code));
        });

        builder.Property(orderItem => orderItem.Quantity)
            .IsRequired();

        // Ignore calculated property
        builder.Ignore(orderItem => orderItem.Subtotal);

        // Shadow property for foreign key
        builder.Property<Guid>("orderId")
            .IsRequired();

        // Ignore domain events
        builder.Ignore(orderItem => orderItem.DomainEvents);
    }
}
