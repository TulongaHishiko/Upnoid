using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upnoid.Domain.Models;

namespace Upnoid.ViewModels.RentalViewModels
{
    public class RentalViewModel
    {
        public List<Movie> Movies { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
