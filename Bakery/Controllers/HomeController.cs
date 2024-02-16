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
    public async Task<ActionResult> Index()
    {
        return View();
    }
}
