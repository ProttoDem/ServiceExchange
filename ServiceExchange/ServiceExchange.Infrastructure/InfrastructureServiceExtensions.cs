using Microsoft.EntityFrameworkCore;
using ServiceExchange.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ServiceExchange.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        ConfigurationManager config,
        ILogger logger)
    {
        services.AddDbContext<AppDbContext>();

        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

        logger.LogInformation("{Project} services registered", "Infrastructure");

        return services;
    }
}