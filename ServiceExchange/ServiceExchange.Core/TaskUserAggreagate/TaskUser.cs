using Ardalis.SharedKernel;
using ServiceExchange.Core.UserAggregate;

namespace ServiceExchange.Core.TaskUserAggreagate;

public class TaskUser : EntityBase<Guid>, IAggregateRoot
{
    public Guid UserId { get; set; }
    public Guid TaskId { get; set; }
    public User User { get; set; } = null!;
    public ServiceExchange.Core.TaskAggregate.Task Task { get; set; } = null!;
    public bool IsOwner { get; set; }
}