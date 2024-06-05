using Ardalis.Specification;
using ServiceExchange.Core.StatusAggregate;

namespace ServiceExchange.Core.TaskAggregate.Specifications;

public class TasksActiveOpenSpec : Specification<Task>
{
    public TasksActiveOpenSpec()
    {
        Query.Where(task => task.TaskUsers.Any(tu => tu.IsOwner) 
                            && task.TaskUsers.All(tu => tu.IsOwner) && task.Status == Status.None);
    }
}