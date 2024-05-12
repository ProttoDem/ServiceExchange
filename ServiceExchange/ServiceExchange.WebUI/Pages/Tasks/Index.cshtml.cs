using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceExchange.WebUI.ViewModels;

namespace ServiceExchange.WebUI.Pages.Tasks;

public class Index : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public Index(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    // Property to hold categories
    //public IEnumerable<CategoryViewModel> CategoriesList { get; set; } = new List<CategoryViewModel>();

    public TasksResponse TasksList = new TasksResponse();

    public class TasksResponse
    {
        [JsonPropertyName("tasks")]
        public IList<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
    }
    
    public async Task OnGet()
    {
        // Create the HTTP client using the FruitAPI named factory
        //var httpClient = _httpClientFactory.CreateClient("ServiceExchangeAPI");
        var msg = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7294/api/Tasks/v1");
        var httpClient = _httpClientFactory.CreateClient();
    
        // Execute the GET operation and store the response, the empty parameter
        // in GetAsync doesn't modify the base address set in the client factory 
        using HttpResponseMessage response = await httpClient.SendAsync(msg);

        // If the operation is successful deserialize the results into the data model
        if (response.IsSuccessStatusCode)
        {
            var contentStream = await response.Content.ReadAsStringAsync();
            TasksList = JsonSerializer.Deserialize<TasksResponse>(contentStream);
        }
    }
}