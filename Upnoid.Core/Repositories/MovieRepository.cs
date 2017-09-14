using System;
using System.Collections.Generic;
using System.Text;
using Upnoid.Core.Abstracts;
using Upnoid.Core.Data;
using Upnoid.Domain.Models;

namespace Upnoid.Core.Repositories
{
    public class MovieRepository:BaseRepository<Movie>,IMovieRepository
    {
        private UpnoidContext _context;

        public MovieRepository(UpnoidContext context) : base(context) { }
    }
}
