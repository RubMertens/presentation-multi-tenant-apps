using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokedex.Framework.Tenants;

namespace Pokedex.Data.Models;

public class Pokemon 
{
    public int Index { get; set; }
    public string Name { get; set; }
}

public class PokemonEntityTypeConfiguration : IEntityTypeConfiguration<Pokemon>
{
    public void Configure(EntityTypeBuilder<Pokemon> builder)
    {
        builder.HasKey(e => new { e.Index });
        builder.Property(e => e.Index).ValueGeneratedNever();
    }
}