using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Upnoid.Domain.Abstracts;

namespace Upnoid.Domain.Models
{
    public class Movie : BaseModel
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Range(1,100)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Number in Stock")]
        [Range(1, 20)]
        public int NumberInStock { get; set; }

        public virtual Trailer Trailer { get; set; }
        public virtual ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
