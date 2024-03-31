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
        //string? connectionString = config.GetConnectionString("DefaultConnection");
        //Guard.Against.Null(connectionString);
        services.AddDbContext<AppDbContext>(
            //options => options.UseSqlServer(config.GetConnectionString("ConnStr"), b=> b.MigrationsAssembly("ServiceExchange.Infrastructure"))
                                            );

        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        //services.AddScoped<IListContributorsQueryService, ListContributorsQueryService>();
        //services.AddScoped<IDeleteContributorService, DeleteContributorService>();

        //services.Configure<MailserverConfiguration>(config.GetSection("Mailserver"));

        logger.LogInformation("{Project} services registered", "Infrastructure");

        return services;
    }
}