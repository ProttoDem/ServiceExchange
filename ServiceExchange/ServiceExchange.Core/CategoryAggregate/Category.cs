using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Task = ServiceExchange.Core.TaskAggregate.Task;

namespace ServiceExchange.Core.CategoryAggregate;

public class Category(string title, string description) : BaseEntity<Guid>, IAggregateRoot
{
    public string Title { get; private set; } = Guard.Against.NullOrEmpty(title, nameof(title));
    public string? Description { get; private set; } = description;
    public ICollection<Task> Tasks = new List<Task>();
    
    public void UpdateTitle(string newTitle)
    {
        Title = Guard.Against.NullOrEmpty(newTitle, nameof(newTitle));
    }
    
    public void UpdateDescription(string newDescription)
    {
        Description = Guard.Against.NullOrEmpty(newDescription, nameof(newDescription));
    }
}