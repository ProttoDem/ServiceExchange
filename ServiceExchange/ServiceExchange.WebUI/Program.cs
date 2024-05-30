using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Graph.Models.ExternalConnectors;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

var builder = WebApplication.CreateBuilder(args);
IEnumerable<string>? initialScopes = builder.Configuration.GetSection("ServiceExchangeScopes:Scopes").Get<string[]>();

builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration, "AzureAd")
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddDownstreamApi("ServiceExchangeApi", builder.Configuration.GetSection("ServiceExchangeScopes"))
    .AddMicrosoftGraph(builder.Configuration.GetSection("MicrosoftGraphScopes"))
    .AddInMemoryTokenCaches();

builder.Services.AddHttpClient();

builder.Services.AddLocalization( options => options.ResourcesPath = "Resources" );

builder.Services.AddRazorPages()
    .AddMvcOptions(options =>
    {
        var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
        options.Filters.Add(new AuthorizeFilter(policy));
        
    })
    .AddMicrosoftIdentityUI()
    .AddViewLocalization();

/*
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole",
        policy => policy.RequireRole("Application Administrator"));
});*/

var app = builder.Build();

var supportedCultures = new List<CultureInfo>
{
    new CultureInfo( "ua" ),
    new CultureInfo( "en" ),
    new CultureInfo( "fr" )
};
var options = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture( "ua" ),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};
app.UseRequestLocalization( options );

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();