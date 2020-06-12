using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreApp31.Controllers
{
    public class RoleController : Controller
    {
        // inject the RoleManager to Create/List roles
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View(new IdentityRole());
        }

        [HttpPost]
        public IActionResult Create(IdentityRole role)
        {
            var res = roleManager.CreateAsync(role).Result;
            return RedirectToAction("Index");
        }
    }
}
