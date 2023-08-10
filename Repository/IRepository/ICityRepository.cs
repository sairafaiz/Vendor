using System.Numerics;
using VendorMVC.Entities;

namespace VendorMVC.Repository.IRepository
{
    public interface ICityRepository : IRepository<City>
    {
        void Update(City obj);
    }
}
