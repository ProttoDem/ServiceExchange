using System.Reflection;
using Ardalis.ListStartupServices;
using Ardalis.SharedKernel;
using FastEndpoints;
using FastEndpoints.Swagger;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Extensions.Logging;
using Service.UseCases.Categories.Update;
using ServiceExchange.Core.CategoryAggregate;
using ServiceExchange.Infrastructure;

var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

logger.Information("Starting web host");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));
var microsoftLogger = new SerilogLoggerFactory(logger)
    .CreateLogger<ServiceExchange.Api.Program>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddMicrosoftIdentityWebApi(options =>
  {
    builder.Configuration.Bind("AzureAd", options);
    options.TokenValidationParameters.NameClaimType = "name";
  }, options => { builder.Configuration.Bind("AzureAd", options); });

builder.Services.AddAuthorization();

// Configure Web Behavior
builder.Services.Configure<CookiePolicyOptions>(options =>
{
  options.CheckConsentNeeded = context => true;
  options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddFastEndpoints().SwaggerDocument(o =>
  {
    o.DocumentSettings = s =>
    {
      s.Title = "ServiceExchangeApi";
      s.Version = "v1";
    };
  });

ConfigureMediatR();

builder.Services.AddInfrastructureServices(builder.Configuration, microsoftLogger);

if (builder.Environment.IsDevelopment())
{
  // Use a local test email server
  // See: https://ardalis.com/configuring-a-local-test-email-server/
  //builder.Services.AddScoped<IEmailSender, MimeKitEmailSender>();

  // Otherwise use this:
  //builder.Services.AddScoped<IEmailSender, FakeEmailSender>();
  AddShowAllServicesSupport();
}
else
{
  //builder.Services.AddScoped<IEmailSender, MimeKitEmailSender>();
}

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseShowAllServicesMiddleware(); // see https://github.com/ardalis/AspNetCoreStartupServices
}
else
{
  app.UseDefaultExceptionHandler(); // from FastEndpoints
  app.UseHsts();
}

app.UseFastEndpoints(c =>
  {
    c.Endpoints.RoutePrefix = "api";
    c.Versioning.Prefix = "v";
  })
    .UseSwaggerGen(); // Includes AddFileServer and static files middleware

app.UseDefaultFiles();
app.UseStaticFiles();

//app.MapRazorPages();

app.UseHttpsRedirection();

app.Run();

void ConfigureMediatR()
{
  var mediatRAssemblies = new[]
{
  Assembly.GetAssembly(typeof(Category)), // Core
  Assembly.GetAssembly(typeof(UpdateCategoryCommand)) // UseCases
};
  builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
  builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
  builder.Services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
}

void AddShowAllServicesSupport()
{
  // add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
  builder.Services.Configure<ServiceConfig>(config =>
  {
    config.Services = new List<ServiceDescriptor>(builder.Services);

    // optional - default path to view services is /listallservices - recommended to choose your own path
    config.Path = "/listservices";
  });
}

// Make the implicit Program.cs class public, so integration tests can reference the correct assembly for host building
namespace ServiceExchange.Api
{
  public partial class Program
  {
  }
}