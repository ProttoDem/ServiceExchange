using Ardalis.Specification;

namespace ServiceExchange.Core.CalendarAggregate.Specifications;

public class CalendarByIdSpec : Specification<Calendar>
{
    public CalendarByIdSpec(Guid calendarId)
    {
        Query.Where(calendar => calendar.Id == calendarId);
    }
}