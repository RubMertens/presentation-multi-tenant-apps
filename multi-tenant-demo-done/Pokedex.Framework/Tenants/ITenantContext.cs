using Pokedex.Framework.Tenants.Options;

namespace Pokedex.Framework.Tenants;

public interface ITenantContext
{
    TenantConfig? Tenant { get; }
    TenantConfig[] AvailableTenants { get; }
}

public class TenantContext : ITenantContext
{
    public TenantConfig? Tenant { get; set; }
    public TenantConfig[] AvailableTenants { get; set; }
}