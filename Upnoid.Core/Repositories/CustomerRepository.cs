using System;
using System.Collections.Generic;
using System.Text;
using Upnoid.Core.Abstracts;
using Upnoid.Core.Data;
using Upnoid.Domain.Models;

namespace Upnoid.Core.Repositories
{
    public class CustomerRepository:BaseRepository<Customer>,ICustomerRepository
    {
        private UpnoidContext _context;

        public CustomerRepository(UpnoidContext context) : base(context) { }
    }
}
