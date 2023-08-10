using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VendorMVC.Entities
{
    public class VendorAddress
    {
        [Key]
        public int AddressID { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? LandMark { get; set; }

        public int? CityID { get; set; }

        [ForeignKey("CityID")]
        public virtual City? City { get; set; }
        
        public int? VendorID { get; set; }

        [ForeignKey("VendorID")]
        public virtual Vendor? Vendor { get; set; }

        [RegularExpression("^[0-9]{6}$", ErrorMessage = "PinCode is not valid")]
        public int PinCode { get; set; }

       
        public string? Session { get; set; }
    }
}
