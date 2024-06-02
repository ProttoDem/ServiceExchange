using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Task = ServiceExchange.Core.TaskAggregate.Task;

namespace ServiceExchange.Core.CalendarAggregate;

public class Calendar : BaseEntity<Guid>, IAggregateRoot
{
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public bool? IsRepeatable { get; set; }
    [ForeignKey("Task")]
    public Guid TaskId { get; set; }
    public Task Task { get; set; } = null!;

    public void UpdateStartTime(DateTime newStartTime)
    {
        StartTime = Guard.Against.NullOrOutOfSQLDateRange(newStartTime, nameof(newStartTime));
    }
    
    public void UpdateEndTime(DateTime newEndTime)
    {
        EndTime = Guard.Against.NullOrOutOfSQLDateRange(newEndTime, nameof(newEndTime));
    }
    
    public void UpdateIsRepeatable(bool newIsRepeatable)
    {
        IsRepeatable = newIsRepeatable;
    }
}