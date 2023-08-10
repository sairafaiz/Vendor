

using System.Numerics;
using VendorMVC.Entities;

namespace VendorMVC.Repository.IRepository
{
    public interface IVendorRepository : IRepository<Vendor>
    {
        void Update(Vendor obj);
    }
}
