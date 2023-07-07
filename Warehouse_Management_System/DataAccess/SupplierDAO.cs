using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class SupplierDAO
    {
        private static SupplierDAO _instance = null;
        private SupplierDAO() { }
        public static SupplierDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SupplierDAO();
            }
            return _instance;
        }

        public List<Supplier> GetSuppliers() => MyDbContext.GetInstance().Suppliers.ToList();
        public Supplier GetCategoryById(string id) => MyDbContext.GetInstance().Suppliers.SingleOrDefault(c => c.ID == id);

        public void SaveSupplier(Supplier supplier)
        {
            try
            {
                MyDbContext.GetInstance().Suppliers.Add(supplier);
                MyDbContext.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateSupplier(Supplier supplier)
        {
            try
            {
                MyDbContext.GetInstance().Entry<Supplier>(supplier).State = EntityState.Modified;
                MyDbContext.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteSupplier(Supplier supplier)
        {
            try
            {
                MyDbContext.GetInstance().Suppliers.Remove(supplier);
                MyDbContext.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
