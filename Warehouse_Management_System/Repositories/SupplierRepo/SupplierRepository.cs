using BusinessObjects;
using DataAccess;

namespace Repositories.SupplierRepo
{
    public class SupplierRepository : ISupplierRepository
    {
        public void SaveSupplier(Supplier supplier) => SupplierDAO.GetInstance.SaveSupplier(supplier);

        public void DeleteSupplier(Supplier supplier) => SupplierDAO.GetInstance.DeleteSupplier(supplier);

        public Supplier GetSupplierById(string id) => SupplierDAO.GetInstance.GetSupplierById(id);

        public Supplier GetSupplierByLastId() => SupplierDAO.GetInstance.GetSupplierByLastId();

        public List<Supplier> GetSuppliers() => SupplierDAO.GetInstance.GetSuppliers();

        public void UpdateSupplier(Supplier supplier) => SupplierDAO.GetInstance.UpdateSupplier(supplier);
    }
}
