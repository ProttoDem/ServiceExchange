using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;

namespace ServiceExchange.WebUI.ViewModels;

public class TaskViewModel(Guid id, string title, string? description, string? imagePath, DateTimeOffset? startTime)
{
    [JsonPropertyName("Id")]
    public Guid Id { get; set; } = id;
    
    [JsonPropertyName("Title")]
    public string Title { get; set; } = title;
    
    [JsonPropertyName("Description")]
    public string? Description { get; set; } = description;
    
    [JsonPropertyName("ImagePath")]
    public string? ImagePath { get; set; } = imagePath.IsNullOrEmpty() ? "images/tasks/default.png" : string.Concat("images/tasks/", imagePath);
    
    [JsonPropertyName("StartTime")]
    public DateTimeOffset? StartTime { get; set; } = startTime;
}