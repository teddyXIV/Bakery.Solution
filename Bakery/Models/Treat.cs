using System.ComponentModel.DataAnnotations;
namespace Bakery.Models;

public class Treat
{
    public int TreatId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    // public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public List<FlavorTreat> JoinEntities { get; }
}