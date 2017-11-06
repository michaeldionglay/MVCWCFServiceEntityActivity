using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Physician_Directory.Models
{
    public class ContactInfo
    {

        public int PhysicianId { get; set; }

        [Display(Name = "Home Address")]
        [Required(ErrorMessage = "Home Address is Required")]
        public string HomeAddress { get; set; }

        [Display(Name = "Home Phone")]
        [Required(ErrorMessage = "Home Phone is required.")]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Home Phone.")]
        public string HomePhone { get; set; }

        [Display(Name = "Office Address")]
        [Required(ErrorMessage = "Office Address is Required")]
        public string OfficeAddress { get; set; }

        [Display(Name = "Office Phone:")]
        [Required(ErrorMessage = "Office Phone is required.")]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Office Phone.")]
        public string OfficePhone { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string EmailAddress { get; set; }

        [Display(Name = "Office Phone")]
        [Required(ErrorMessage = "Office Phone is required.")]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Office Phone.")]
        public string CellphoneNumber { get; set; }
    }
}