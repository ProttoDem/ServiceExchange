namespace ServiceExchange.Api.Tasks;

public class GetTasksActiveOpenResponse
{
    public IEnumerable<TaskRecord> Tasks { get; set; } = [];
}