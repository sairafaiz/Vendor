

using System.Numerics;
using VendorMVC.Entities;

namespace VendorMVC.Repository.IRepository
{
    public interface IVendorAddressRepository : IRepository<VendorAddress>
    {
        void Update(VendorAddress obj);
    }
}
