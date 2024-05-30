using Ardalis.SharedKernel;
using ServiceExchange.Core.RoleAggregate;
using ServiceExchange.Core.TaskUserAggreagate;

namespace ServiceExchange.Core.UserAggregate;

public class User(string systemId) : BaseEntity<Guid>, IAggregateRoot
{
    public string SystemId { get; private set; } = systemId;
    public List<Role> Roles { get; } = [];
    public List<TaskUser> TaskUsers { get; } = [];
}