using Ardalis.Specification;

namespace ServiceExchange.Core.TaskAggregate.Specifications;

public class TaskByIdSpec : Specification<Task>
{
    public TaskByIdSpec(Guid taskId)
    {
        Query.Where(task => task.Id == taskId);
    }
}