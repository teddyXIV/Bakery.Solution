using Microsoft.AspNetCore.Identity;

namespace Bakery.Models;

public class ApplicationUser : IdentityUser
{
    public List<Order> Orders { get; set; }
}
