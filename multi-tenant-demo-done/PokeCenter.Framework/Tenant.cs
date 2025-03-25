using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Pokedex.Framework;

public class Tenant
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public string CityImage { get; set; }
}

public class AvailableTenants
{
    public const string SectionName = nameof(AvailableTenants);
    public Tenant[] Tenants { get; set; }
}

public class AvailableTenantsSetup(IConfiguration configuration) : IConfigureOptions<AvailableTenants>
{
    public void Configure(AvailableTenants options)
    {
        configuration.GetSection(AvailableTenants.SectionName).Bind(options);
    }
}

public class TenantContext
{
    public Tenant Tenant { get; set; } = new();
}

public static class TenantExtensions
{
    public static IServiceCollection AddTenantContext(this IServiceCollection services)
    {
        services.AddScoped<TenantContext>();
        services.AddTransient<Tenant>(p => p.GetRequiredService<TenantContext>().Tenant);
        services.AddSingleton<IConfigureOptions<AvailableTenants>, AvailableTenantsSetup>();
        return services;
    }
}