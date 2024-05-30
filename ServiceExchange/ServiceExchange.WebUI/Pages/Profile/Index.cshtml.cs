using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using ServiceExchange.WebUI.ViewModels;

namespace ServiceExchange.WebUI.Pages.Profile;

[Authorize]
[AuthorizeForScopes(Scopes = new []{"user.read"})]
public class Index : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    private readonly GraphServiceClient _graphServiceClient;
    
    private readonly IHttpClientFactory _httpClientFactory;
    
    public TasksResponse TasksList = new();

    public Index(ILogger<PrivacyModel> logger,
        GraphServiceClient graphServiceClient,
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _graphServiceClient = graphServiceClient;
        _httpClientFactory = httpClientFactory;
    }

    public async Task OnGet()
    {
        var user = await _graphServiceClient.Me.GetAsync();
        ViewData["user"] = user;
        ViewData["name"] = user.DisplayName;
        ViewData["birthDate"] = user.Birthday;
        
        var msg = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7294/api/user/tasks/v1");
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

public class TasksResponse
{
    [JsonPropertyName("tasks")]
    public IList<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
}
