using Microsoft.Extensions.DependencyInjection;

namespace Pokedex.Framework.Tenants.DependencyInjection;

public static class TenantContextAccessorExtensions
{
    public static IServiceCollection AddTenantContext(this IServiceCollection services)
    {
        services.AddScoped<ITenantContextAccessor>();
        services.AddTransient<ITenantContext>(p => p.GetRequiredService<ITenantContextAccessor>().TenantContext);
        return services;
    }
}