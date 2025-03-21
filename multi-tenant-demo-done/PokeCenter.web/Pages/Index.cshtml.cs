using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pokedex.Framework.Tenants;
using Pokedex.Framework.Tenants.DependencyInjection;

namespace PokeCenter.web.Pages;

public class IndexModel(
    ILogger<IndexModel> logger,
    ITenantContext tenantContext
) : PageModel
{
    private readonly ILogger<IndexModel> _logger = logger;

    public string Tenant { get; } = tenantContext.Tenant.Name;
    public string Background { get; set; } = tenantContext.Tenant.Background;

    public void OnGet()
    {
    }
}