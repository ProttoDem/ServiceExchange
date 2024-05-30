namespace ServiceExchange.Api.Tasks;

public class GetTasksByUserResponse
{
    public IEnumerable<TaskRecord> Tasks { get; set; } = [];
}