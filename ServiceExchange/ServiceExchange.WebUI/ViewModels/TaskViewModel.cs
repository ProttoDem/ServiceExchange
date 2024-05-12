using System.Text.Json.Serialization;

namespace ServiceExchange.WebUI.ViewModels;

public class TaskViewModel(Guid id, string title, string? description)
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; } = id;
    
    [JsonPropertyName("title")]
    public string Title { get; set; } = title;
    
    [JsonPropertyName("description")]
    public string? Description { get; set; } = description;
}