using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Bakery.Models;

namespace Identity.Controllers;

public class RoleController : Controller
{
    private RoleManager<IdentityRole> roleManager;
    private UserManager<ApplicationUser> userManager;
    public RoleController(RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userMrg)
    {
        roleManager = roleMgr;
        userManager = userMrg;
    }

    public ViewResult Index() => View(roleManager.Roles);

    private void Errors(IdentityResult result)
    {
        foreach (IdentityError error in result.Errors)
            ModelState.AddModelError("", error.Description);
    }

    public async Task<IActionResult> Update(string id)
    {
        IdentityRole role = await roleManager.FindByIdAsync(id);
        List<ApplicationUser> members = new List<ApplicationUser>();
        List<ApplicationUser> nonMembers = new List<ApplicationUser>();
        foreach (ApplicationUser user in userManager.Users)
        {
            var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
            list.Add(user);
        }
        return View(new RoleEdit
        {
            Role = role,
            Members = members,
            NonMembers = nonMembers
        });
    }

    [HttpPost]
    public async Task<IActionResult> Update(RoleModification model)
    {
        IdentityResult result;
        if (ModelState.IsValid)
        {
            foreach (string userId in model.AddIds ?? new string[] { })
            {
                ApplicationUser user = await userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    result = await userManager.AddToRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                        Errors(result);
                }
            }
            foreach (string userId in model.DeleteIds ?? new string[] { })
            {
                ApplicationUser user = await userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                        Errors(result);
                }
            }
        }

        if (ModelState.IsValid)
            return RedirectToAction("Index");
        else
            return await Update(model.RoleId);
    }
}
