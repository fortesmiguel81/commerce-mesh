using OrderService.Application.Abstractions.Authentication;

namespace OrderService.Infrastructure.Authentication;

internal sealed class UserContext : IUserContext
{
    /*private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }*/

    public UserContext()
    {
    }

    // Force a specific user id for demonstration purposes (00000000-0000-0000-0000-000000000001)
    public Guid UserId => Guid.Parse("00000000-0000-0000-0000-000000000001");
}
