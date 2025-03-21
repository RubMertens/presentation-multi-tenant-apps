using Microsoft.AspNetCore.Identity;
using Pokedex.Data.Models;

namespace Pokedex.Data;

public class ApplicationUser : IdentityUser, ITenanted
{
    public string TenantId { get; set; }
}