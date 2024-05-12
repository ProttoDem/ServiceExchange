using ServiceExchange.Core.CalendarAggregate;
using ServiceExchange.Core.CategoryAggregate;
using ServiceExchange.Core.StatusAggregate;
using ServiceExchange.Core.UserAggregate;

namespace Service.UseCases.Tasks;

public record TaskDTO(Guid Id, string Title, string? Description, Calendar Calendar, Category Category, double Price, Status Status, List<User> Users);