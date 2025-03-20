using System.Dynamic;
using System.Linq.Expressions;
using System.Security.Cryptography.Xml;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.IdentityModel.Abstractions;
using Pokedex.Data.Models;
using Pokedex.Framework.Tenants;

namespace Pokedex.Data;

public class ApplicationUser : IdentityUser, ITenanted
{
    public string TenantId { get; set; }
}

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    ITenantContext tenantContext)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<HealingPod> Pods { get; set; }
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<PokemonAdmission> Admissions { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        //entity type configurations can't have constructor parameters, so we have to set the tenant id filter here
        builder.Entity<PokemonAdmission>().HasQueryFilter(e => e.TenantId == tenantContext.Tenant.Id);
        builder.Entity<HealingPod>().HasQueryFilter(e => e.TenantId == tenantContext.Tenant.Id);

        // foreach (var entityType in builder.Model.GetEntityTypes())
        // {
        //     if (typeof(ITenanted).IsAssignableFrom(entityType.ClrType))
        //     {
        //         builder.Entity(entityType.ClrType)
        //             .HasQueryFilter(GetTenantFilterExpression(entityType.ClrType));
        //     }
        // }

        base.OnModelCreating(builder);
    }

    private LambdaExpression GetTenantFilterExpression(Type entityType)
    {
        var param = Expression.Parameter(entityType, "e");
        var prop = Expression.Property(Expression.Convert(param, typeof(ITenanted)), nameof(ITenanted.TenantId));
        var tenantConst = Expression.Constant(tenantContext?.Tenant?.Id ?? string.Empty);
        var body = Expression.Equal(prop, tenantConst);
        return Expression.Lambda(body, param);
    }

    internal string GetTenantId()
    {
        return tenantContext.Tenant.Id;
    }
}

class TenantIdValueGenerator : ValueGenerator<string>
{
    public override string Next(EntityEntry entry)
    {
        if (entry.Context is ApplicationDbContext context)
        {
            return context.GetTenantId();
        }

        return string.Empty;
    }

    public override bool GeneratesTemporaryValues => false;
}