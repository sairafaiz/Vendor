using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using VendorMVC.Entities;

namespace VendorMVC.Models
{
    public class StateVM
    {
        public State State { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? CountryList { get; set; }
    }
}
