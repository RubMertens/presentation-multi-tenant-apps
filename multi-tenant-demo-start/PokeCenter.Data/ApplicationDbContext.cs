using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Pokedex.Data.Models;
using Pokedex.Framework;

namespace Pokedex.Data;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    TenantContext tenantContext
)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<HealingPod> Pods { get; set; }
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<PokemonAdmission> Admissions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var tenantedModels = builder.Model.GetEntityTypes()
                .Where(entity => typeof(ITenanted).IsAssignableFrom(entity.ClrType))
            ;

        foreach (var tenantedModel in tenantedModels)
        {
            builder.Entity(tenantedModel.ClrType)
                .HasQueryFilter<ITenanted>(e => e.TenantId == TenantId)
                .HasIndex(nameof(ITenanted.TenantId))
                ;
            builder.Entity(tenantedModel.ClrType)
                .Property(nameof(ITenanted.TenantId))
                .IsRequired()
                .HasValueGenerator<TenantIdValueGenerator>()
                ;
        }


        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // builder.Entity<HealingPod>().HasQueryFilter(e => e.TenantId == TenantId);
        // builder.Entity<PokemonAdmission>().HasQueryFilter(e => e.TenantId == TenantId);

        base.OnModelCreating(builder);
    }

    public string TenantId => tenantContext.Tenant.Id;
}

public static class QueryFilterExtensions
{
    public static EntityTypeBuilder HasQueryFilter<TInterface>(this EntityTypeBuilder entityTypeBuilder,
        Expression<Func<TInterface, bool>> filterExpression)
    {
        var param = Expression.Parameter(entityTypeBuilder.Metadata.ClrType);
        var body = ReplacingExpressionVisitor.Replace(filterExpression.Parameters.Single(), param,
            filterExpression.Body);

        var lambdaExpression = Expression.Lambda(body, param);

        return entityTypeBuilder.HasQueryFilter(lambdaExpression);
    }
}

public class TenantIdValueGenerator : ValueGenerator<string>
{
    public override string Next(EntityEntry entry)
    {
        if (entry is { Entity: ITenanted, Context: ApplicationDbContext appDbContext })
        {
            return appDbContext.TenantId;
        }

        throw new InvalidOperationException("Could not generate a new TenantId");
    }

    public override bool GeneratesTemporaryValues { get; }
        = false;
}