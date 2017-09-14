using System;
using System.Collections.Generic;
using System.Text;
using Upnoid.Core.Abstracts;
using Upnoid.Core.Data;
using Upnoid.Domain.Models;

namespace Upnoid.Core.Repositories
{
    public class TrailerRepository:BaseRepository<Trailer>,ITrailerRepository
    {
        private UpnoidContext _context;

        public TrailerRepository(UpnoidContext context) : base(context) { }
    }
}
