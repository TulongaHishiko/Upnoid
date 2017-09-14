using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upnoid.Domain.Models;

namespace Upnoid.ViewModels.UserManagmentViewModels
{
    public class UserManagementIndexViewModel
    {
        public List<ApplicationUser> Users { get; set; }
    }
}
