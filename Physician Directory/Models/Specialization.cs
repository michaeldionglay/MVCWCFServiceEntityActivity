using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Physician_Directory.Models
{
    public class Specialization
    {
       
        public int PhysicianId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Specialization Name is Required")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }

       
    }
}