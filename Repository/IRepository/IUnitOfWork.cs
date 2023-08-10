
namespace VendorMVC.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IVendorRepository Vendor { get; }
        ICountryRepository Country { get; }

        IStateRepository State { get; }

        ICityRepository City { get; }

        IVendorAddressRepository VendorAddress { get; }
       
        void save();
    }
}
