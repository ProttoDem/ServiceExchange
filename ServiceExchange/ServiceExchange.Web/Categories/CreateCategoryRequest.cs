using System.ComponentModel.DataAnnotations;

namespace ServiceExchange.Web.Categories;

public class CreateCategoryRequest
{
    public const string Route = "/Categories";

    [Required] 
    public string? Title { get; set; }
    
    public string? Description { get; set; }
}