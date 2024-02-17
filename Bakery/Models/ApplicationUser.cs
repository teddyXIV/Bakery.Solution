using Microsoft.AspNetCore.Identity;

namespace Bakery.Models;

public class ApplicationUser : IdentityUser
{
    public string Alias { get; set; }
}
