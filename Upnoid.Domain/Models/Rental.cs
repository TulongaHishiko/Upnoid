using System;
using System.ComponentModel.DataAnnotations;
using Upnoid.Domain.Abstracts;

namespace Upnoid.Domain.Models
{
    public class Rental : BaseModel
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public virtual Customer Customer { get; set; }

        [Required]
        public int MovieId { get; set; }
        [Required]
        public virtual Movie Movie { get; set; }

        public DateTime DateRented { get; set; }

        public DateTime? DateReturned { get; set; }
    }
}
