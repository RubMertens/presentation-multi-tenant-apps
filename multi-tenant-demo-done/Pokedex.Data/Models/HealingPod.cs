using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokedex.Framework.Tenants;

namespace Pokedex.Data.Models;

public class HealingPod 
{
    public int Id { get; set; }
    public int MaxCapacity { get; set; }
    public List<PokemonAdmission> Admission { get; set; }
    // public string TenantId { get; set; }
}

public class HealingPodEntityTypeConfiguration : IEntityTypeConfiguration<HealingPod>
{
    public void Configure(EntityTypeBuilder<HealingPod> builder)
    {
        builder.HasKey(e => new { e.Id });
        // builder.Property(e => e.TenantId).HasValueGenerator<TenantIdValueGenerator>();
        // builder.HasIndex(e => e.TenantId);
    }
}