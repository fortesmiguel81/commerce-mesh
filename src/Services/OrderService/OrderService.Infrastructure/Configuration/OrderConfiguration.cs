using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Orders;
using OrderService.Domain.Shared;

namespace OrderService.Infrastructure.Configuration;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        // Configure table name
        builder.ToTable("orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.UserId)
            .IsRequired();

        builder.Property(o => o.OrderDate)
            .IsRequired();

        // Configure Money value object
        builder.OwnsOne(order => order.TotalAmount, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code,
                    code => Currency.FromCode(code));
        });

        builder.Property(o => o.Status)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(o => o.Priority)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(o => o.CreatedOnUtc)
            .IsRequired();

        builder.Property(o => o.ConfirmedOnUtc);
        builder.Property(o => o.ProcessingOnUtc);
        builder.Property(o => o.ShippedOnUtc);
        builder.Property(o => o.DeliveredOnUtc);
        builder.Property(o => o.CancelledOnUtc);
        builder.Property(o => o.ReturnedOnUtc);
        builder.Property(o => o.RefundedOnUtc);
        builder.Property(o => o.FailedOnUtc);
        builder.Property(o => o.OnHoldOnUtc);

        // Configure the navigation property with backing field
        builder.Navigation(o => o.Items)
            .HasField("_items")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        // Configure relationship with OrderItems
        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey("orderId")
            .OnDelete(DeleteBehavior.Cascade);

        // Ignore domain events
        builder.Ignore(o => o.DomainEvents);
    }
}
