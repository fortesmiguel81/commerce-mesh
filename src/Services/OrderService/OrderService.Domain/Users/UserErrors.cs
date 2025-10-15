using OrderService.SharedKernel;

namespace OrderService.Domain.Users;

public static class UserErrors
{
    public static Error Unauthorized() => Error.Failure(
        "Users.Unauthorized",
        "You are not authorized to perform this action.");
}
