using System.Text.Json.Serialization;
using ServiceExchange.WebUI.ViewModels;

namespace ServiceExchange.WebUI.Responses;

public class CategoriesResponse
{
    [JsonPropertyName("categories")]
    public IList<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
}
