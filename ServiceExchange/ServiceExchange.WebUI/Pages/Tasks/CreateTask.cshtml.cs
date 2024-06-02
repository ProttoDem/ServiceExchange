using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text.Json;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Abstractions;
using ServiceExchange.WebUI.ViewModels;

namespace ServiceExchange.WebUI.Pages.Tasks;

public class CreateTask : PageModel
{
    [BindProperty]
    [Required]
    public string? Title { get; set; }
    
    [BindProperty]
    public string? Description { get; set; }

    [BindProperty]
    [Required]
    public DateTime StartTime { get; set; }
    
    [BindProperty]
    public DateTime? FinishTime { get; set; }
    
    [BindProperty]
    [Required]
    public double Price { get; set; }

    [BindProperty]
    [Required]
    public Guid CategoryId { get; set; }

    [BindProperty]
    public bool IsRepeatable { get; set; }
    
    private readonly ILogger<PrivacyModel> _logger;
    
    private readonly IDownstreamApi  _downstreamWebApi;
    
    private TaskViewModel Task;

    public CreateTask(ILogger<PrivacyModel> logger,
        IDownstreamApi  downstreamWebApi
    )
    {
        _logger = logger;
        _downstreamWebApi = downstreamWebApi;
    }
    
    public void OnGet()
    {
        
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var createTaskRequest = new CreateTaskRequest
        {
            Title = this.Title,
            Description = this.Description,
            StartTime = this.StartTime,
            FinishTime = this.FinishTime,
            Price = this.Price,
            CategoryId = this.CategoryId,
            IsRepeatable = this.IsRepeatable
        };

        TaskViewModel? response;
        try
        {
            response = await _downstreamWebApi.CallApiForUserAsync<CreateTaskRequest, TaskViewModel>(
                "ServiceExchangeApi", createTaskRequest, options =>
                {
                    options.HttpMethod = "POST";
                    options.RelativePath = $"task/v1";
                }).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            var error = e;
            throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {error}");
        }
        
        return RedirectToPage("/task/" + response.Id); // Redirect to a success page after form submission
    }
}

public class CreateTaskRequest
{
    public const string Route = "/task";

    [Required] 
    public string? Title { get; set; }
    
    public string? Description { get; set; }

    [Required] 
    public DateTime StartTime { get; set; }
    
    public DateTime? FinishTime { get; set; }
    
    [Required]
    public double Price { get; set; }

    [Required]
    public Guid CategoryId { get; set; }

    public bool? IsRepeatable { get; set; }
}