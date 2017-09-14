using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Upnoid.ViewModels.MovieViewModels
{
        public class MovieViewModel
        {
            public int? Id { get; set; }

            [Required]
            [StringLength(255)]
            public string Name { get; set; }

            [Display(Name = "Genre")]
            [Required]
            public string Genre { get; set; }

            [Display(Name = "Release Date")]
            [DataType(DataType.Date)]
            [Required]
            public DateTime? ReleaseDate { get; set; }

            [DataType(DataType.Time)]
            [Display(Name = "Date Added")]
            public DateTime DateAdded { get; set; }

            [Required]
            [Range(1, 100)]
            [DataType(DataType.Currency)]
            public double Price { get; set; }

            [Display(Name = "Number in Stock")]
            [Range(1, 20)]
            [Required]
            public int? NumberInStock { get; set; }
        }
    }
