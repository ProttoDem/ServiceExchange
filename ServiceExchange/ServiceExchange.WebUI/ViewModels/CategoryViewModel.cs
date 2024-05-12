﻿using System.Text.Json.Serialization;

namespace ServiceExchange.WebUI.ViewModels;

public class CategoryViewModel(Guid id, string title, string? description)
{
    [JsonPropertyName("Id")]
    public Guid Id { get; set; } = id;
    
    [JsonPropertyName("Title")]
    public string Title { get; set; } = title;
    
    [JsonPropertyName("Description")]
    public string? Description { get; set; } = description;
}