using System.ComponentModel.DataAnnotations;
namespace Bakery.Models;

public class Flavor
{
    public int FlavorId { get; set; }
    [Required]
    public string Name { get; set; }
    public ApplicationUser User { get; set; }
    public List<FlavorTreat> JoinEntities { get; }
}