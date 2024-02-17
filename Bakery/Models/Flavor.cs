using System.ComponentModel.DataAnnotations;
namespace Bakery.Models;

public class Flavor
{
    public int FlavorId { get; set; }
    [Required(ErrorMessage = "You must add a flavor name!")]
    public string Name { get; set; }
    // public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public List<FlavorTreat> JoinEntities { get; }
}