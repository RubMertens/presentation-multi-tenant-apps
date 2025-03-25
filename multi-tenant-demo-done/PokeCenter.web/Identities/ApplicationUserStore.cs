using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pokedex.Data;
using Pokedex.Data.Models;
using Pokedex.Framework;

namespace PokeCenter.web.Identities;

public class ApplicationUserStore(ApplicationDbContext context, Tenant tenant, IdentityErrorDescriber? describer = null)
    : UserStore<ApplicationUser>(context, describer)
{
    public override Task<IdentityResult> CreateAsync(ApplicationUser user,
        CancellationToken cancellationToken = new CancellationToken())
    {
        user.TenantId = tenant.Id;
        return base.CreateAsync(user, cancellationToken);
    }
};