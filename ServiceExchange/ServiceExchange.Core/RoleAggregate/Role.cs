using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using ServiceExchange.Core.UserAggregate;

namespace ServiceExchange.Core.RoleAggregate;

public class Role : BaseEntity<Guid>, IAggregateRoot
{
    public string Title { get; set; } = null!;
    public List<User> Users { get; } = [];
}