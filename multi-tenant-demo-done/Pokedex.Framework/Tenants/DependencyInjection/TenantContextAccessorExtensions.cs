using Microsoft.Extensions.DependencyInjection;

namespace Pokedex.Framework.Tenants.DependencyInjection;

public static class TenantContextAccessorExtensions
{
    public static IServiceCollection AddTenantContext(this IServiceCollection services)
    {
        services.AddScoped<TenantContextAccessor>();
        services.AddTransient<ITenantContext>(p =>
            p.GetRequiredService<TenantContextAccessor>().TenantContext ?? new TenantContext());
        return services;
    }
}