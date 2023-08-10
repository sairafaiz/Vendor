using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Xml.Linq;

namespace VendorMVC.Entities
{
    public class Vendor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Notes { get; set; }

        [ValidateNever]
        public string? imageurl { get; set; }

        [Required]
        public int? CityID { get; set; }

        [ForeignKey("CityID")]
        public City? City { get; set; }
    }
}
