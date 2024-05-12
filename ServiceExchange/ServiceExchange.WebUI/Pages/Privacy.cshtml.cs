using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;
using ServiceExchange.WebUI.ViewModels;

namespace ServiceExchange.WebUI.Pages;

[Authorize]
[AuthorizeForScopes(Scopes = new string[]{"api://2a778855-3e11-4f00-b599-2721a05bf807/ServiceExchange.Admin"})]
public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    private readonly IDownstreamApi  _downstreamWebApi;

    public PrivacyModel(ILogger<PrivacyModel> logger,
        IDownstreamApi  downstreamWebApi)
    {
        _logger = logger;
        _downstreamWebApi = downstreamWebApi;
    }

    public async Task OnGet()
    {
        var response = await _downstreamWebApi.GetForUserAsync<CategoriesViewModel>("ServiceExchangeApi", 
            options =>
        {
          options.RelativePath = $"Categories/v1";
        });
        if (response != null)
        {
            ViewData["ApiResult"] = response;
        }
        else
        {
            throw new HttpRequestException($"Invalid status code in the HttpResponseMessage:");
        }
    }
}