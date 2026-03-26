using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pokedex.Data.Models;

public class PokemonAdmission 
{
    public int Id { get; set; }
    public Pokemon Pokemon { get; set; }
    public int PokemonId { get; set; }
    public DateTime AdmissionStart { get; set; }
    public DateTime AdmissionEnd { get; set; }
    public string Trainer { get; set; }

    public HealingPod Pod { get; set; }
    public int PodId { get; set; }
}

