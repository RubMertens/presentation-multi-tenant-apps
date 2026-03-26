using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pokedex.Data.Models;

public class Pokemon 
{
    public int Index { get; set; }
    public string Name { get; set; }
}