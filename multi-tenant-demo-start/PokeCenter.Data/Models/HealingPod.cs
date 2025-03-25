using System.Threading.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pokedex.Data.Models;

public class HealingPod 
{
    public int Id { get; set; }
    public int MaxCapacity { get; set; }
    public List<PokemonAdmission> Admission { get; set; }
}

public class HealingPodEntityTypeConfiguration : IEntityTypeConfiguration<HealingPod>
{
    public void Configure(EntityTypeBuilder<HealingPod> builder)
    {
        builder.HasKey(e => new { e.Id });
    }
}