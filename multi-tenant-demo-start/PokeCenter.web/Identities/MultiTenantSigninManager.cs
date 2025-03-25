using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Pokedex.Data.Models;
using Pokedex.Framework;

namespace PokeCenter.web.Identities;

public class MultiTenantSigninManager(
    UserManager<ApplicationUser> userManager,
    IHttpContextAccessor contextAccessor,
    IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
    IOptions<IdentityOptions> optionsAccessor,
    ILogger<SignInManager<ApplicationUser>> logger,
    IAuthenticationSchemeProvider schemes,
    IUserConfirmation<ApplicationUser> confirmation,
    Tenant tenant
)
    : SignInManager<ApplicationUser>(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes,
        confirmation)
{
    public override Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent,
        bool lockoutOnFailure)
    {
        if (user.TenantId != tenant.Id)
        {
            return Task.FromResult(SignInResult.Failed);
        }

        return base.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
    }
};