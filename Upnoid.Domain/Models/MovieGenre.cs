using System;
using System.Collections.Generic;
using System.Text;
using Upnoid.Domain.Abstracts;

namespace Upnoid.Domain.Models
{
    public class MovieGenre:BaseModel
    {
        public int MovieID { get; set; }
        public virtual Movie Movie { get; set; }
        public int GenreID { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
