using System.ComponentModel.DataAnnotations;

namespace ServiceExchange.Categories;

public class UpdateCategoryRequest
{
    public const string Route = "/Categories/{CategoryId:Guid}";
    public static string BuildRoute(Guid categoryId) => Route.Replace("{CategoryId:Guid}", categoryId.ToString());

    public Guid CategoryId { get; set; }
    
    [Required]
    public string? Title { get; set; }
    
    public string? Description { get; set; }
}