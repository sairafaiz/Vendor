using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using VendorMVC.Entities;

namespace VendorMVC.Models
{
    public class CityVM
    {
        //public int? Id { get; set; }

        //public string Name { get; set; }

        public City City { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? StateList { get; set; }
    }
}
