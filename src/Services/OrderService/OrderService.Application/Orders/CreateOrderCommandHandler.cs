using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Abstractions.Authentication;
using OrderService.Application.Abstractions.Data;
using OrderService.Domain.Orders;
using OrderService.Domain.Shared;
using OrderService.Domain.Users;
using OrderService.SharedKernel;

namespace OrderService.Application.Orders;

internal sealed class CreateOrderCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider,
    IUserContext userContext
) : ICommandHandler<CreateOrderCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        if (userContext.UserId != command.UserId)
        {
            return Result.Failure<Guid>(UserErrors.Unauthorized());
        }

        // Possible future enhancement: check if user exists in UserService via IUserServiceClient

        var currency = Currency.FromCode(command.Currency);

        var orderItems = command.Items.Select(item =>
            new OrderItem(
                item.ProductId,
                item.ProductName,
                new Money(item.UnitPrice, currency),
                item.Quantity
            )).ToList();

        var order = Order.Create(
            command.UserId,
            dateTimeProvider.UtcNow,
            (Priority)command.Priority,
            orderItems
        );

        context.Orders.Add(order);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(order.Id);
    }
}
