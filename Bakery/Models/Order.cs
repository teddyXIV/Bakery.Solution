using System.ComponentModel.DataAnnotations;
namespace Bakery.Models;

public class Order
{
    public int OrderId { get; set; }
    [Required]
    public string Treat { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public int Price { get; set; }
    public ApplicationUser User { get; set; }
}