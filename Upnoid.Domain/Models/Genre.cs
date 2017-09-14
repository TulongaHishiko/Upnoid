using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Upnoid.Domain.Abstracts;

namespace Upnoid.Domain.Models
{
    public class Genre:BaseModel
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public virtual List<MovieGenre> MovieGenres { get; set; }
    }
}
