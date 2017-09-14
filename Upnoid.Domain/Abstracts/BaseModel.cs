using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Upnoid.Domain.Abstracts
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; }
    }
}
