using System.Threading.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pokedex.Data.Models;

public class HealingPod : ITenanted
{
    public int Id { get; set; }
    public int MaxCapacity { get; set; }
    public List<PokemonAdmission> Admission { get; set; }
    public string TenantId { get; set; }
}

public class HealingPodEntityTypeConfiguration : IEntityTypeConfiguration<HealingPod>
{
    public void Configure(EntityTypeBuilder<HealingPod> builder)
    {
        builder.HasKey(e => new { e.Id });

        // builder.Property(e => e.TenantId).IsRequired()
        //     .HasValueGenerator<TenantIdValueGenerator>()
        //     ;
        // builder.HasIndex(e => e.TenantId);
        
    }
}