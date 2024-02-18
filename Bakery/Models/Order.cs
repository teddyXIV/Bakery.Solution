using System.ComponentModel.DataAnnotations;
namespace Bakery.Models;

public class Order
{
    public int OrderId { get; set; }
    // [Required(ErrorMessage = "You must add a flavor name!")]
    public string Treat { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public ApplicationUser User { get; set; }
}