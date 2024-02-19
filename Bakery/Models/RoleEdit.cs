using Microsoft.AspNetCore.Identity;

namespace Bakery.Models;

public class RoleEdit
{
    public IdentityRole Role { get; set; }
    public IEnumerable<ApplicationUser> Members { get; set; }
    public IEnumerable<ApplicationUser> NonMembers { get; set; }
}