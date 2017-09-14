using System;
using System.ComponentModel.DataAnnotations;
using Upnoid.Domain.Helpers;
using Upnoid.Domain.Abstracts;

namespace Upnoid.Domain.Models
{
    public class Customer:BaseModel
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(20, ErrorMessage = "First name cannot be longer than 20 characters.")]
        [MinLength(1, ErrorMessage = "Last name cannot 1 be a character.")]
        public string FirstName { get; set; }
            
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20, ErrorMessage = "First name cannot be longer than 20 characters.")]
        [MinLength(1, ErrorMessage = "First name cannot 1 be a character.")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [Min18YearsIfAMember]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]  
        public DateTime? Birthdate { get; set; }

        [Display(Name = "Subscribed to Newsletter")]
        public bool IsSubcribedToNewsletter { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
