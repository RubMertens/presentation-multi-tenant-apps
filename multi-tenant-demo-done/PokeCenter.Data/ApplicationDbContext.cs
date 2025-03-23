using System.Dynamic;
using System.Linq.Expressions;
using System.Security.Cryptography.Xml;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.IdentityModel.Abstractions;
using Pokedex.Data.Models;
using Pokedex.Framework.Tenants;
using Pokedex.Framework.Tenants.DependencyInjection;

namespace Pokedex.Data;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    TenantContextAccessor tenantContextAccessor)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<HealingPod> Pods { get; set; }
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<PokemonAdmission> Admissions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        //entity type configurations can't have constructor parameters, so we have to set the tenant id filter here
        builder.Entity<ApplicationUser>().HasQueryFilter(e => e.TenantId == GetTenantId());
        builder.Entity<PokemonAdmission>().HasQueryFilter(e => e.TenantId == GetTenantId());
        builder.Entity<HealingPod>().HasQueryFilter(e => e.TenantId == GetTenantId());

        base.OnModelCreating(builder);
    }


    internal string? GetTenantId()
    {
        return tenantContextAccessor.TenantContext?.Tenant?.Id;
    }
}

class TenantIdValueGenerator : ValueGenerator<string>
{
    public override string Next(EntityEntry entry)
    {
        if (entry.Context is ApplicationDbContext context)
        {
            var tenantId = context.GetTenantId();
            if (tenantId == null)
                throw new InvalidOperationException("TenantId is exptected to be set on this entity!");
            return tenantId;
        }

        return string.Empty;
    }

    public override bool GeneratesTemporaryValues => false;
}