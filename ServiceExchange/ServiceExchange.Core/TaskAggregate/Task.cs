using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using ServiceExchange.Core.CalendarAggregate;
using ServiceExchange.Core.CategoryAggregate;
using ServiceExchange.Core.StatusAggregate;
using ServiceExchange.Core.TaskUserAggreagate;

namespace ServiceExchange.Core.TaskAggregate;

public class Task(string title, string? description, double price, Status status) : BaseEntity<Guid>, IAggregateRoot
{
    public string Title { get; private set; } = Guard.Against.NullOrEmpty(title, nameof(title));
    public string? Description { get; private set; } = Guard.Against.NullOrEmpty(description, nameof(description));
    public Calendar Calendar { get; set; } = null!;
    public Guid CalendarId { get; set; }
    public Category Category { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public double Price { get; private set; } = Guard.Against.Negative(price, nameof(price));
    public Status Status { get; private set; } = status;
    public List<TaskUser> TaskUsers { get; } = [];
}