
using VendorMVC.Data;
using VendorMVC.Entities;
using VendorMVC.Repository.IRepository;

namespace VendorMVC.Repository
{
    public class VendorRepository : Repository<Vendor>, IVendorRepository
    {

        private ApplicationDbContext _db;

        public VendorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Vendor obj)
        {
            var objfromdb = _db.Vendor.FirstOrDefault(u => u.Id == obj.Id);

            if (objfromdb != null)
            {
                objfromdb.Name = obj.Name;
                objfromdb.Email = obj.Email;
                objfromdb.PhoneNumber = obj.PhoneNumber;
                objfromdb.Notes = obj.Notes;
                objfromdb.imageurl = obj.imageurl;
            }
        }

    }
}
