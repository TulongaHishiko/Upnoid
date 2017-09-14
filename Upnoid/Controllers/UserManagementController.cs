using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Upnoid.Core.Data;
using Upnoid.Domain.Models;
using Upnoid.ViewModels.UserManagmentViewModels;

namespace Upnoid.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly UpnoidContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UserManagementController(UpnoidContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vm = new UserManagementIndexViewModel()
            {
                Users = _context.Users.OrderBy(m => m.Email).Include(u=>u.Roles).ToList()
            };
            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> AddRole(string id)
        {
            var user = await GetUserById(id);
            var vm = new UserManagementAddRoleViewModel
            {
                Roles = GetAllRoles(),
                UserId = id,
                Email = user.Email
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(UserManagementAddRoleViewModel rvm)
        {
            var user = await GetUserById(rvm.UserId);
            if (ModelState.IsValid)
            {
                var results = await _userManager.AddToRoleAsync(user, rvm.NewRole);
                if (results.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            rvm.Email = user.Email;
            rvm.Roles = GetAllRoles();
            return View(rvm);       
        }
        private async Task<ApplicationUser> GetUserById(string id) =>
             await _userManager.FindByIdAsync(id);

        private SelectList GetAllRoles() => new SelectList(_roleManager.Roles.OrderBy(r => r.Name));
        }
    }
