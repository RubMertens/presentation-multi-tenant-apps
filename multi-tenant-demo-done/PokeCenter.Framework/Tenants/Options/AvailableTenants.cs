using System.Diagnostics.CodeAnalysis;

namespace Pokedex.Framework.Tenants.Options;

public class AvailableTenants
{
    public const string SectionName = nameof(AvailableTenants);
    public TenantConfig[] Tenants { get; set; }
}