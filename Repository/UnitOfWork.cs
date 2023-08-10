using VendorMVC.Data;
using VendorMVC.Repository.IRepository;

namespace VendorMVC.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IVendorRepository Vendor { get; private set; }
        public ICountryRepository Country { get; private set; }
        public IStateRepository State { get; private set; }

        public ICityRepository City { get; private set; }

        public IVendorAddressRepository VendorAddress { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Vendor = new VendorRepository(_db);
            Country = new CountryRepository(_db);
            State = new StateRepository(_db);
            City = new CityRepository(_db);
            VendorAddress = new VendorAddressRepository(_db);
        }

        public void save()
        {
            _db.SaveChanges();
        }
    }
}






