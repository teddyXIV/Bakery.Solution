using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Bakery.Models;

namespace Bakery.Controllers;

public class HomeController : Controller
{
    private readonly BakeryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(UserManager<ApplicationUser> userManager, BakeryContext db)
    {
        _userManager = userManager;
        _db = db;
    }

    [HttpGet("/")]
    public async Task<IActionResult> Index()
    {
        ViewBag.User = await _userManager.GetUserAsync(HttpContext.User);
        Flavor[] flavs = _db.Flavors.ToArray();
        Treat[] treats = _db.Treats.ToArray();
        Dictionary<string, object[]> model = new Dictionary<string, object[]>
        {
            { "flavors", flavs },
            { "treats", treats }
        };
        return View(model);
    }
}
