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

