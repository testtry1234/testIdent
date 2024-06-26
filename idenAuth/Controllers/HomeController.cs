using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using idenAuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using idenAuth.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace idenAuth.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    UserManager<IdentityUser>  userManager;
    RoleManager<IdentityRole>  roleManager;
    ApplicationDbContext db;

    public HomeController(ILogger<HomeController> logger,
    UserManager<IdentityUser>  user,
    RoleManager<IdentityRole>  role,
    ApplicationDbContext test){
        _logger = logger;
        userManager = user;
        roleManager = role;
        db= test;
    }

    public async Task<IActionResult> Index()
    {
        //var users = userManager.Users.ToList();
        //await roleManager.CreateAsync(new IdentityRole{Name = "Admin"});
        //await roleManager.CreateAsync(new IdentityRole{Name="SuperAdmin"});
        //await roleManager.CreateAsync(new IdentityRole{Name ="Reporter"});
        //await roleManager.CreateAsync(new IdentityRole{Name="SalesManager"});
        //if(db.Roles.FirstOrDefault(x => x.Name == "bbb") == null){

        //db.Roles.Add(new IdentityRole{Name = "bbb",NormalizedName="BBB"});
        //db.SaveChanges();
        //}
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> AddRole(RoleVM model)
    {
        await roleManager.CreateAsync(new IdentityRole{Name = model.roleName});

        return RedirectToAction("Index");
    }
    public async Task<IActionResult> UserRoles()
    {
        var users =await userManager.Users.ToListAsync();
        List<UserRolesVm> result= new List<UserRolesVm>();
        foreach(var item in users){
            var roles = await userManager.GetRolesAsync(item);
    
            result.Add(new UserRolesVm {user =item,userRoles=(List<string>)roles
            });
        }
        ViewBag.allRoles = await roleManager.Roles.ToListAsync();
        return View(result);
    }
    public IActionResult Users()
    {
        var users = userManager.Users.ToList();
        //userManager.CreateAsync(new IdentityUser{});
        return View(users);
    }
    public IActionResult Roles()
    {
        var roles = roleManager.Roles.ToList();
        //roleManager.CreateAsync(new IdentityRole{});
        return View(roles);
    }
    [Authorize]
    public IActionResult Privacy()
    {
        return View();
    }
    [Authorize(Roles ="Admin")]
    public IActionResult Sales()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
