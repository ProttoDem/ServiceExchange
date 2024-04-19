using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace ServiceExchange.Core.CalendarAggregate;

public class Calendar(DateTime startTime, DateTime endTime, bool isRepeatable) : BaseEntity<Guid>, IAggregateRoot
{
    public DateTime StartTime { get; private set; } = Guard.Against.NullOrOutOfSQLDateRange(startTime, nameof(startTime));
    public DateTime? EndTime { get; private set; } = Guard.Against.OutOfSQLDateRange(endTime, nameof(endTime));
    public bool? IsRepeatable { get; private set; } = isRepeatable;

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