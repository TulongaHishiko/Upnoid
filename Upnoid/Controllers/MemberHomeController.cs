using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Upnoid.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MemberHomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AccessGranted()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult AnonymousAccess()
        {
            return View();
        }
    }
}
