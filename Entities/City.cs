using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Xml.Linq;

namespace VendorMVC.Entities
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int StateID { get; set; }

        [ForeignKey("StateID")]
        public State? State { get; set; }

    }
}
