using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Graph;
using Microsoft.Identity.Abstractions;
using Newtonsoft.Json;
using ServiceExchange.WebUI.Responses;
using ServiceExchange.WebUI.ViewModels;

namespace ServiceExchange.WebUI.Pages.Tasks;

[AllowAnonymous]
public class Index : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;
    
    private readonly IHttpClientFactory _httpClientFactory;
    
    public TasksResponse TasksList = new();

    public UserViewModel User = new();

    public Index(ILogger<PrivacyModel> logger,
        IHttpClientFactory httpClientFactory
    )
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }
    
    public async Task OnGet()
    {
        // Create the HTTP client using the FruitAPI named factory
        //var httpClient = _httpClientFactory.CreateClient("ServiceExchangeAPI");
        var msg = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7294/api/tasks/v1");
        var httpClient = _httpClientFactory.CreateClient();
    
        // Execute the GET operation and store the response, the empty parameter
        // in GetAsync doesn't modify the base address set in the client factory 
        using HttpResponseMessage response = await httpClient.SendAsync(msg);

        // If the operation is successful deserialize the results into the data model
        if (response.IsSuccessStatusCode)
        {
            var contentStream = await response.Content.ReadAsStringAsync();
            TasksList = JsonConvert.DeserializeObject<TasksResponse>(contentStream);
        }
    }
}