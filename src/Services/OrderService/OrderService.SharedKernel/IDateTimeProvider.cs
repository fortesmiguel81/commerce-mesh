namespace OrderService.SharedKernel;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
