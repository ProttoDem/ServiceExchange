using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Graph;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;
using ServiceExchange.WebUI.Responses;
using ServiceExchange.WebUI.ViewModels;

namespace ServiceExchange.WebUI.Pages.Profile;

[Authorize]
[AuthorizeForScopes(Scopes = new []{"user.read"})]
public class Index : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    private readonly GraphServiceClient _graphServiceClient;
    
    private readonly IHttpClientFactory _httpClientFactory;
    
    private readonly IDownstreamApi  _downstreamWebApi;
    
    public TasksResponse TasksList = new();

    public UserViewModel User = new();

    public Index(ILogger<PrivacyModel> logger,
        GraphServiceClient graphServiceClient,
        IDownstreamApi  downstreamWebApi
        )
    {
        _logger = logger;
        _graphServiceClient = graphServiceClient;
        _downstreamWebApi = downstreamWebApi;
    }

    public async Task OnGet()
    {
        var user = await _graphServiceClient.Me.GetAsync();
        User.DisplayName = user.DisplayName;
        User.Birthdate = user.Birthday;
        User.Phone = user.MobilePhone;
        
        using var response = await _downstreamWebApi.CallApiForUserAsync("ServiceExchangeApi", options =>
            {
                options.RelativePath = $"user/v1";
            }).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            await GetTasks();
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}: {error}");
        }
    }

    private async Task GetTasks()
    {
        using var response = await _downstreamWebApi.CallApiForUserAsync("ServiceExchangeApi", options =>
        {
            options.RelativePath = $"user/tasks/v1";
        }).ConfigureAwait(false);
        
        if (response.IsSuccessStatusCode)
        {
            var apiResult = await response.Content.ReadFromJsonAsync<JsonDocument>().ConfigureAwait(false);
            ViewData["ApiResult"] = JsonSerializer.Serialize(apiResult, new JsonSerializerOptions { WriteIndented = true });
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}: {error}");
        }
    }
}
