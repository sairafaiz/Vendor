
using System.Linq.Expressions;
using VendorMVC.Data;
using VendorMVC.Entities;
using VendorMVC.Repository.IRepository;

namespace VendorMVC.Repository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {

        private ApplicationDbContext _db;

        public CountryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

      
        public void Update(Country obj)
        {
            var objfromdb = _db.Country.FirstOrDefault(u => u.Id == obj.Id);

            if (objfromdb != null)
            {
                objfromdb.Name = obj.Name;
                objfromdb.Code = obj.Code;
                objfromdb.flag = obj.flag;
            }
        }

    }
}
