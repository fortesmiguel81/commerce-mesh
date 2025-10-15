using Application.Abstractions.Messaging;
using OrderService.Api.Extensions;
using OrderService.Api.Infrastructure;
using OrderService.Application.Orders;
using OrderService.SharedKernel;
using Web.Api.Endpoints;

namespace OrderService.Api.Endpoints.Orders;

internal sealed class Create : IEndpoint
{
    public sealed class Request
    {
        public Guid UserId { get; set; }
        public List<OrderItemDto> Items { get; set; } = [];
        public string Currency { get; set; }
        public int Priority { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("orders", async (
            Request request,
            ICommandHandler<CreateOrderCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateOrderCommand
            {
                UserId = request.UserId,
                Currency = request.Currency,
                Priority = request.Priority,
                Items = request.Items.Select(item => new CreateOrderItemCommand
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Currency = item.Currency,
                    Quantity = item.Quantity
                }).ToList()
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                orderId => Results.Created($"/orders/{orderId}", new { OrderId = orderId }),
                CustomResults.Problem);
        });
    }
}
