using System.Text.Json.Serialization;

namespace ServiceExchange.WebUI.ViewModels;

public class CategoriesViewModel
{
    [JsonPropertyName("Categories")]
    public IEnumerable<CategoryViewModel> Categories { get; set; }
}