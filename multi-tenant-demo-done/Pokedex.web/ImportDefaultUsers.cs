using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Pokedex.Data;
using Pokedex.Framework.Tenants;
using Pokedex.Framework.Tenants.DependencyInjection;
using Pokedex.Framework.Tenants.Options;

namespace Pokedex.web;

public class ImportDefaultUsers(IServiceProvider provider)
{
    public async Task Import()
    {
        await CreatePalletTownNurseJoy();
        await CreateVermillionCityNurseJoy();
    }

    private async Task CreatePalletTownNurseJoy()
    {
        using var scope = provider.CreateScope();
        scope.ServiceProvider.GetRequiredService<TenantContextAccessor>().TenantContext = new TenantContext()
        {
            Tenant = new TenantConfig()
            {
                Id = "pallet-town"
            }
        };

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var email = "nurse-joy@pallet-town.com";
        var palletTownNurseJoy = await userManager.FindByEmailAsync(email);
        if (palletTownNurseJoy == null)
        {
            var result = await userManager.CreateAsync(new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                TenantId = "pallet-town"
            }, "Test123.");

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
        }
    }

    private async Task CreateVermillionCityNurseJoy()
    {
        using var scope = provider.CreateScope();
        scope.ServiceProvider.GetRequiredService<TenantContextAccessor>().TenantContext = new TenantContext()
        {
            Tenant = new TenantConfig()
            {
                Id = "vermillion-city"
            }
        };

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var email = "nurse-joy@vermillion-city.com";
        if (await userManager.FindByEmailAsync(email) == null)
        {
            var result = await userManager.CreateAsync(new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                TenantId = "vermillion-city"
            }, "Test123.");

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
        }
    }
}