using Microsoft.AspNetCore.Identity;

namespace Pokedex.Data.Models;

public class ApplicationUser : IdentityUser, ITenanted
{
    public string TenantId { get; set; }
}