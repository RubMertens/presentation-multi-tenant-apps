using Microsoft.Extensions.Options;
using Pokedex.Framework;

namespace PokeCenter.web.Middleware;

public class TenantMiddleware(TenantContext tenantContext, IOptionsSnapshot<AvailableTenants> availableTenants)
    : IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var subdomain = context.Request.Host.Host.Split('.')[0];

        var matchingTenant = availableTenants.Value.Tenants.FirstOrDefault(t => t.Id == subdomain);
        if (matchingTenant == null)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return Task.CompletedTask;
        }

        tenantContext.Tenant = matchingTenant;
        return next(context);
    }
}