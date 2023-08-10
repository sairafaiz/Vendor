using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Xml.Linq;

namespace VendorMVC.Entities
{
    public class Country
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Code { get; set; }

        [ValidateNever]
        public string? flag { get; set; }

    }
}
