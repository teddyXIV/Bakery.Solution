using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Bakery.Models;

public class BakeryContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Flavor> Flavors { get; set; }
    public DbSet<Treat> Treat { get; set; }
    public DbSet<FlavorTreat> FlavorTreats { get; set; }
    public BakeryContext(DbContextOptions options) : base(options) { }

}