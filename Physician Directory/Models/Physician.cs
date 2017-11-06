using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Physician_Directory.Models
{
    public class Physician
    {


        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is Required")]

        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        [Required(ErrorMessage = "Middle Name is Required")]
        public string MiddleName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:MM'/'dd'/'yyyy}", ApplyFormatInEditMode = true)]

        public DateTime BirthDate { get; set; }

        public string Gender { get; set; }

        [Display(Name = "Weight")]
        public double? Weight { get; set; }
        [Display(Name = "Height")]
        public double? Height { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public Specialization Specialization { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

    }
    }
    
    
