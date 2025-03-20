using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pokedex.Data;
using Pokedex.Framework.Tenants;

namespace Pokedex.web.MultiTenancy.Identity;

public class ApplicationUserStore(
    ApplicationDbContext context,
    ITenantContext tenantContext,
    IdentityErrorDescriber? describer = null)
    : UserStore<ApplicationUser>(context, describer)
{
    public override Task<IdentityResult> CreateAsync(ApplicationUser user,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (tenantContext.Tenant == null)
            return Task.FromResult(IdentityResult.Failed(new IdentityError
                { Code = "TenantNotFound", Description = "Tenant not found" }));
        user.TenantId = tenantContext.Tenant.Id;
        return base.CreateAsync(user, cancellationToken);
    }
};

public class MultiTenantSigninManager(
    UserManager<ApplicationUser> userManager,
    IHttpContextAccessor contextAccessor,
    IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
    IOptions<IdentityOptions> optionsAccessor,
    ILogger<SignInManager<ApplicationUser>> logger,
    IAuthenticationSchemeProvider schemes,
    IUserConfirmation<ApplicationUser> confirmation,
    ITenantContext tenantContext
)
    : SignInManager<ApplicationUser>(userManager,
        contextAccessor,
        claimsFactory,
        optionsAccessor,
        logger,
        schemes,
        confirmation)
{
    public async override Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password,
        bool isPersistent, bool lockoutOnFailure)
    {
        if (tenantContext.Tenant == null)
            return SignInResult.Failed;
        if (user.TenantId != tenantContext.Tenant?.Id)
            return SignInResult.Failed;

        return await base.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
    }
}