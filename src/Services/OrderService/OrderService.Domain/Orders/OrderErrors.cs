using OrderService.SharedKernel;

namespace OrderService.Domain.Orders;

public static class OrderErrors
{
    public static Error NotPending = new(
        "Order.NotPending",
        "The order is not in pending status.",
        ErrorType.Validation);

    public static Error NotFound = new(
        "Order.NotFound",
        "The order was not found.",
        ErrorType.NotFound);

    public static Error NotConfirmed = new(
        "Order.NotConfirmed",
        "The order is not confirmed.",
        ErrorType.Validation);

    public static Error AlreadyConfirmed = new(
        "Order.AlreadyConfirmed",
        "The order is already confirmed.",
        ErrorType.Conflict);
}
