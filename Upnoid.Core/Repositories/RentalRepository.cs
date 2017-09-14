using System;
using System.Collections.Generic;
using System.Text;
using Upnoid.Core.Abstracts;
using Upnoid.Core.Data;
using Upnoid.Domain.Models;

namespace Upnoid.Core.Repositories
{
    public class RentalRepository:BaseRepository<Rental>,IRentalRepository
    {
        private UpnoidContext _context;

        public RentalRepository(UpnoidContext context) : base(context) { }
    }
}
