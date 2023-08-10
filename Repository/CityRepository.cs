
using System.Linq.Expressions;
using VendorMVC.Data;
using VendorMVC.Entities;
using VendorMVC.Repository.IRepository;

namespace VendorMVC.Repository
{
    public class CityRepository : Repository<City>, ICityRepository
    {

        private ApplicationDbContext _db;

        public CityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

      
        public void Update(City obj)
        {
            var objfromdb = _db.Cities.FirstOrDefault(u => u.Id == obj.Id);

            if (objfromdb != null)
            {
                objfromdb.Name = obj.Name;
            }
        }

    }
}
