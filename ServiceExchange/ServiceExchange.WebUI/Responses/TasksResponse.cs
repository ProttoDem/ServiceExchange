using System.Text.Json.Serialization;
using ServiceExchange.WebUI.ViewModels;

namespace ServiceExchange.WebUI.Responses;

public class TasksResponse
{
    [JsonPropertyName("tasks")]
    public IList<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
}
