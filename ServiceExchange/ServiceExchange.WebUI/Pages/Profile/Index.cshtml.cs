using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;
using ServiceExchange.WebUI.ViewModels;

namespace ServiceExchange.WebUI.Pages.Profile;

[Authorize]
[AuthorizeForScopes(Scopes = new []{"user.read"})]
public class Index : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    private readonly IDownstreamApi  _downstreamWebApi;

    public Index(ILogger<PrivacyModel> logger,
        IDownstreamApi  downstreamWebApi)
    {
        _logger = logger;
        _downstreamWebApi = downstreamWebApi;
    }

    public async Task OnGet()
    {
        var response = await _downstreamWebApi.CallApiForUserAsync("GraphApi").ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            ViewData["ApiResult"] = response;
        }
        else
        {
            throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response}");
        }
    }
}