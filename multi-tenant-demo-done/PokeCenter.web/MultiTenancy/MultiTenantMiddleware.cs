using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pokedex.Framework;
using Pokedex.Framework.Tenants;
using Pokedex.Framework.Tenants.DependencyInjection;
using Pokedex.Framework.Tenants.Options;

namespace PokeCenter.web.MultiTenancy;

public class MultiTenantMiddleware(
    TenantContextAccessor tenantContextAccessor,
    IOptionsSnapshot<AvailableTenants> tenantConfiguration) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        //subdomain based
        var subDomain = context.Request.Host.Host.Split('.')[0];
        var matchingTenant = tenantConfiguration.Value.Tenants.FirstOrDefault(t => t.Id == subDomain);
        if (matchingTenant != null)
        {
            tenantContextAccessor.TenantContext = new TenantContext
            {
                Tenant = matchingTenant,
                AvailableTenants = tenantConfiguration.Value.Tenants
            };
            await next(context);
        }
        else
        {
            context.Response.StatusCode = 404;
            return;
        }
    }
}