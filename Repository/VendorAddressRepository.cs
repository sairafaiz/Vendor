
using VendorMVC.Data;
using VendorMVC.Entities;
using VendorMVC.Repository.IRepository;

namespace VendorMVC.Repository
{
    public class VendorAddressRepository : Repository<VendorAddress>, IVendorAddressRepository
    {

        private ApplicationDbContext _db;

        public VendorAddressRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(VendorAddress obj)
        {
            var objfromdb = _db.vendorAddresses.FirstOrDefault(u => u.AddressID == obj.AddressID);

            if (objfromdb != null)
            {
                objfromdb.AddressLine1 = obj.AddressLine1;
                objfromdb.AddressLine2 = obj.AddressLine2;
                objfromdb.LandMark = obj.LandMark;
                objfromdb.CityID = obj.CityID;
                objfromdb.VendorID = obj.VendorID;
                objfromdb.PinCode = obj.PinCode;
                objfromdb.Session = obj.Session;
            }
        }

    }
}
