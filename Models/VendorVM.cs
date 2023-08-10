using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VendorMVC.Entities;

namespace VendorMVC.Models
{
    public class VendorVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression("^[6-9]{1}[0-9]{9}$", ErrorMessage = "Phone Number is not valid")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Notes { get; set; }

        [ValidateNever]
        public string? imageurl { get; set; }

        //[Required]
        public int? CityID { get; set; }

        [Required]
        public int StateID { get; set; }

        [Required]
        public int CountryID { get; set; }


        // Address Details

        public VendorAddress? VendorAddress { get; set; }

    }
}
