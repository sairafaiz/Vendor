using System.Numerics;
using VendorMVC.Entities;

namespace VendorMVC.Repository.IRepository
{
    public interface IStateRepository : IRepository<State>
    {
        void Update(State obj);
    }
}
