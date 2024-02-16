using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using Bakery.Models;

namespace Bakery.Controllers;

public class FlavorsController : Controller
{
    private readonly BakeryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public FlavorsController(UserManager<ApplicationUser> userManager, BakeryContext db)
    {
        _userManager = userManager;
        _db = db;
    }

    public ActionResult Index()
    {
        List<Flavor> model = _db.Flavors.ToList();
        return View(model);
    }

    [Authorize]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Flavor flav)
    {
        if (!ModelState.IsValid)
        {
            return View(flav);
        }
        else
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            flav.User = currentUser;
            _db.Flavors.Add(flav);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

    public ActionResult Details(int id)
    {
        Flavor flav = _db.Flavors
            .Include(flav => flav.JoinEntities)
            .ThenInclude(join => join.Treat)
            .FirstOrDefault(flav => flav.FlavorId == id);
        return View(flav);
    }

    [Authorize]
    public ActionResult Edit(int id)
    {
        Flavor flav = _db.Flavors.FirstOrDefault(flav => flav.FlavorId == id);
        return View(flav);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flav)
    {
        _db.Flavors.Update(flav);
        _db.SaveChanges();
        return RedirectToAction("Details", new { id = flav.FlavorId });
    }

    [Authorize]
    [HttpPost]
    public ActionResult Delete(int id)
    {
        Flavor flav = _db.Flavors.FirstOrDefault(flav => flav.FlavorId == id);
        _db.Flavors.Remove(flav);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult AddTreat(int id)
    {
        Flavor flav = _db.Flavors.FirstOrDefault(flav => flav.FlavorId == id);
        ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
        return View(flav);
    }
    [HttpPost]
    public ActionResult AddTreat(Flavor flav, int treatId)
    {
#nullable enable
        FlavorTreat? joinEntity = _db.FlavorTreats.FirstOrDefault(join => (join.TreatId == treatId && join.FlavorId == flav.FlavorId));
#nullable disable
        if (joinEntity == null && treatId != 0)
        {
            _db.FlavorTreats.Add(new FlavorTreat() { TreatId = treatId, FlavorId = flav.FlavorId });
            _db.SaveChanges();
        }
        return RedirectToAction("Details", new { id = flav.FlavorId });
    }

    [Authorize]
    [HttpPost]
    public ActionResult DeleteJoin(int joinId, int flavId)
    {
        FlavorTreat joinEntry = _db.FlavorTreats.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
        _db.FlavorTreats.Remove(joinEntry);
        _db.SaveChanges();
        return RedirectToAction("Details", new { id = flavId });
    }
}
