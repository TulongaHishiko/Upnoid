using System;
using System.Collections.Generic;
using System.Text;
using Upnoid.Core.Abstracts;
using Upnoid.Core.Data;
using Upnoid.Domain.Models;

namespace Upnoid.Core.Repositories
{
    public class GenreRepository:BaseRepository<Genre>,IGenreRepository
    {
        private UpnoidContext _context;

        public GenreRepository(UpnoidContext context) : base(context) { }
    }
}
