using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pokedex.Data.Models;

namespace Pokedex.Data;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options
)
    : IdentityDbContext(options)
{
    public DbSet<HealingPod> Pods { get; set; }
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<PokemonAdmission> Admissions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Pokemon>(entity =>
        {
            entity.HasKey(e => new { e.Index });
            entity.Property(e => e.Index).ValueGeneratedNever();
        });

        builder.Entity<HealingPod>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
        builder.Entity<PokemonAdmission>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
        base.OnModelCreating(builder);
    }
}
