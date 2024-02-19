using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using Bakery.Models;

namespace Bakery.Controllers;

public class TreatsController : Controller
{
    private readonly BakeryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(UserManager<ApplicationUser> userManager, BakeryContext db)
    {
        _userManager = userManager;
        _db = db;
    }

    public ActionResult Index()
    {
        List<Treat> model = _db.Treats.ToList();
        return View(model);
    }

    [Authorize]
    public ActionResult Create()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult> Create(Treat treat)
    {
        if (!ModelState.IsValid)
        {
            return View(treat);
        }
        else
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            treat.User = currentUser;
            _db.Treats.Add(treat);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

    public ActionResult Details(int id)
    {
        Treat treat = _db.Treats
            .Include(treat => treat.JoinEntities)
            .ThenInclude(join => join.Flavor)
            .FirstOrDefault(treat => treat.TreatId == id);
        return View(treat);
    }

    [Authorize]
    public ActionResult Edit(int id)
    {
        Treat treat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
        return View(treat);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult Edit(Treat treat)
    {
        _db.Treats.Update(treat);
        _db.SaveChanges();
        return RedirectToAction("Details", new { id = treat.TreatId });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult Delete(int id)
    {
        Treat treat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
        _db.Treats.Remove(treat);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult AddFlavor(int id)
    {
        Treat treat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
        ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
        return View(treat);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult AddFlavor(Treat treat, int flavorId)
    {
#nullable enable
        FlavorTreat? joinEntity = _db.FlavorTreats.FirstOrDefault(join => (join.FlavorId == flavorId && join.TreatId == treat.TreatId));
#nullable disable
        if (joinEntity == null && flavorId != 0)
        {
            _db.FlavorTreats.Add(new FlavorTreat() { TreatId = treat.TreatId, FlavorId = flavorId });
            _db.SaveChanges();
        }
        return RedirectToAction("Details", new { id = treat.TreatId });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult DeleteJoin(int joinId, int treatId)
    {
        FlavorTreat joinEntry = _db.FlavorTreats.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
        _db.FlavorTreats.Remove(joinEntry);
        _db.SaveChanges();
        return RedirectToAction("Details", new { id = treatId });
    }

}
