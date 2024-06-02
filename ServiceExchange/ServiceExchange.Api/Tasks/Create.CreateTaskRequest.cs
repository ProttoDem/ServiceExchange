using System.ComponentModel.DataAnnotations;

namespace ServiceExchange.Api.Tasks;

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