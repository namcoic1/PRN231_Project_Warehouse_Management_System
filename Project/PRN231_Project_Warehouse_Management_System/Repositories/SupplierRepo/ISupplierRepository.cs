using BusinessObjects;

namespace Repositories.SupplierRepo
{
    public interface ISupplierRepository
    {
        List<Supplier> GetSuppliers();
        Supplier GetSupplierById(string id);
        void SaveSupplier(Supplier supplier);
        void UpdateSupplier(Supplier supplier);
        void DeleteSupplier(Supplier supplier);
    }
}
