using Ardalis.Specification;

namespace ServiceExchange.Core.TaskAggregate.Specifications;

public class TasksByUserIdSpec: Specification<Task>
{
    public TasksByUserIdSpec(string userId)
    {
        Query.Where(task => task.TaskUsers.Any(user => user.User.SystemId == userId));
    }
}