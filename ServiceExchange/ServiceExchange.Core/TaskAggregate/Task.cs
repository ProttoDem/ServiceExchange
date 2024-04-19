using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using ServiceExchange.Core.CalendarAggregate;
using ServiceExchange.Core.CategoryAggregate;
using ServiceExchange.Core.StatusAggregate;
using ServiceExchange.Core.UserAggregate;

namespace ServiceExchange.Core.TaskAggregate;

public class Task(string title, string? description, Calendar calendar, Category category, double price, Status status, User worker, User owner) : BaseEntity<Guid>, IAggregateRoot
{
    public string Title { get; private set; } = Guard.Against.NullOrEmpty(title, nameof(title));
    public string? Description { get; private set; } = Guard.Against.NullOrEmpty(description, nameof(description));
    public Calendar Calendar { get; private set; } = calendar;
    public Category Category { get; private set; } = category;
    public double Price { get; private set; } = Guard.Against.Negative(price, nameof(price));
    public Status Status { get; private set; } = status;
    public User Worker { get; private set; } = worker;
    public User Owner { get; private set; } = owner;

}