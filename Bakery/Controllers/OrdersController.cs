using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using Bakery.Models;

namespace Bakery.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private readonly BakeryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public OrdersController(UserManager<ApplicationUser> userManager, BakeryContext db)
    {
        _userManager = userManager;
        _db = db;
    }

    public async Task<ActionResult> Index()
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser user = await _userManager.FindByIdAsync(userId);
        List<Order> model = _db.Orders
            .Where(entry => entry.User.Id == user.Id).
            Include(order => order.User)
            .ToList();
        return View(model);
    }

    [Authorize]
    public ActionResult Create()
    {
        ViewBag.Treat = new SelectList(_db.Treats, "Name", "Name");
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Order order)
    {
        if (!ModelState.IsValid)
        {
            return View(order);
        }
        else
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            order.User = currentUser;
            _db.Orders.Add(order);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

    public ActionResult Details(int id)
    {
        Order order = _db.Orders
            .FirstOrDefault(order => order.OrderId == id);
        return View(order);
    }

    [Authorize]
    public ActionResult Edit(int id)
    {
        ViewBag.Treat = new SelectList(_db.Treats, "Name", "Name");
        Order order = _db.Orders.FirstOrDefault(order => order.OrderId == id);
        return View(order);
    }

    [HttpPost]
    public ActionResult Edit(Order order)
    {
        _db.Orders.Update(order);
        _db.SaveChanges();
        return RedirectToAction("Details", new { id = order.OrderId });
    }

    [Authorize]
    [HttpPost]
    public ActionResult Delete(int id)
    {
        Order order = _db.Orders.FirstOrDefault(order => order.OrderId == id);
        _db.Orders.Remove(order);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

}
