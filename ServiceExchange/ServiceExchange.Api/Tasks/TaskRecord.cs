namespace ServiceExchange.Api.Tasks;

public record TaskRecord(Guid Id, string Title, string? Description, DateTime? DateStart);