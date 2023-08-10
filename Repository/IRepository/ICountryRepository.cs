using System.Numerics;
using VendorMVC.Entities;

namespace VendorMVC.Repository.IRepository
{
    public interface ICountryRepository : IRepository<Country>
    {
        void Update(Country obj);
    }
}
